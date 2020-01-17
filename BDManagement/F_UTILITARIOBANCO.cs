using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CustomControls.Data;
using FirebirdSql.Data.Services;
using FirebirdSql.Data.FirebirdClient;

namespace Conection
{
    public partial class F_UTILITARIOBANCO : Form
    {
        string bancoOriginal,
               string_conexao,
               bancoTemp = Directory.GetCurrentDirectory() + @"\Temp\bd_temp.fdb";
        bool reorganizacao = false, refazCopiaSeg = false, cancelouCopia = false, bloqueiaFechamento = false,
             realizouCopiaFisica = false, realizouBackup = false, realizouReorganizacao = false;

        #region Backup Firebird
        private void buttonSelecionarArquivoBackup_Click(object sender, EventArgs e)
        {
            sFDialogArquivoBackup.FileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".gbk";

            if (sFDialogArquivoBackup.ShowDialog(this) == DialogResult.OK)
            {
                textBoxArquivoBackup.Text = sFDialogArquivoBackup.FileName;
            }
        }
        
        private void buttonRealizaBackup_Click(object sender, EventArgs e)
        {
            if (textBoxArquivoBackup.Text == "")
            {
                Mensagem.Aviso(this, "É necessário selecionar o local do arquivo de backup para que seja possível realizar o backup !");
                return;
            }
            FbConnection.ClearAllPools();

            BloqueiaControlesBackup();
            bloqueiaFechamento = true;
            RealizandoBackup(@textBoxArquivoBackup.Text, true);
            bloqueiaFechamento = false;
            BloqueiaControlesBackup();
        }

        private void BloqueiaControlesBackup()
        {
            textBoxArquivoBackup.Enabled = !textBoxArquivoBackup.Enabled;
            buttonSelecionarArquivoBackup.Enabled = !buttonSelecionarArquivoBackup.Enabled;
            buttonRealizaBackup.Enabled = !buttonRealizaBackup.Enabled;
        }

        private void RealizandoBackup(string arquivoBackup, bool mostraMsg)
        {
            // Feito dessa maneira para carregar as mensagens depois do aguarde
            bool resultado = false;
            Exception excecao = null;

            using (ClassAguarde carregando = new ClassAguarde())
            {
                try
                {
                    resultado = BackupDatabaseFirebird(@arquivoBackup);
                }
                catch (Exception ex)
                {
                    excecao = ex;
                }
            }

            if (resultado)
            {
                if (mostraMsg)
                    Mensagem.Aviso(this, "Backup Realizado com sucesso !");

                if (!reorganizacao)
                    realizouBackup = true;
            }
            else
            {
                if (mostraMsg)
                    Mensagem.Excecao(this, "Erro ao realizar o backup.", excecao);
                else
                    throw new Exception(excecao.Message);
            }
        }

        private void backupSvc_ServiceOutput(object sender, ServiceOutputEventArgs e)
        {
            if (reorganizacao)
                 EscreveLinhaListBox(listBoxInfoReorganizacao, e.Message);
            else
                 EscreveLinhaListBox(listBoxInfoBackup, e.Message);
        }

        private bool BackupDatabaseFirebird(string arquivoBackup)
        {
            try
            {
                listBoxInfoBackup.Items.Clear();
                string_conexao = @"User=" + Conexao.USUARIO + ";Password=" + Conexao.SENHA + ";Database=" + Conexao.SERVER + Conexao.BANCO + ";DataSource=localhost;Port=3050;Dialect=3;";

                FbBackup backupSvc = new FbBackup();
                backupSvc.ServiceOutput += new ServiceOutputEventHandler(backupSvc_ServiceOutput);
                backupSvc.ConnectionString = string_conexao;
                backupSvc.BackupFiles.Add(new FbBackupFile(arquivoBackup, 2048));
                backupSvc.Verbose = true;
                backupSvc.Options = FbBackupFlags.IgnoreLimbo;
                backupSvc.Execute();
                backupSvc = null;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Restore Firebird
        private void buttonSelecionarArquivoRestauracao_Click(object sender, EventArgs e)
        {
            if (oFDialogArquivoRestauracao.ShowDialog(this) == DialogResult.OK)
            {
                textBoxArquivoRestauracao.Text = oFDialogArquivoRestauracao.FileName;
            }
        }

        private void buttonRealizaRestauração_Click(object sender, EventArgs e)
        {
            if (textBoxArquivoRestauracao.Text == "")
            {
                Mensagem.Aviso(this, "É necessário selecionar o arquivo de backup para que seja possível realizar a restauração !");
                return;
            }
            FbConnection.ClearAllPools();
            bloqueiaFechamento = true;
            RealizandoRestore(@textBoxArquivoRestauracao.Text, true);
            bloqueiaFechamento = false;
        }

        private void BloqueiaControlesRestauracao()
        {
            textBoxArquivoRestauracao.Enabled = !textBoxArquivoRestauracao.Enabled;
            buttonRealizaReorganizacao.Enabled = !buttonRealizaReorganizacao.Enabled;
            buttonSelecionarArquivoRestauracao.Enabled = !buttonSelecionarArquivoRestauracao.Enabled;
        }

        private void RealizandoRestore(string arquivoBackup, bool mostraMsg)
        {
            // Feito dessa maneira para carregar as mensagens depois do aguarde
            bool resultado = false;
            Exception excecao = null;

            CopiaSeguranca();
            if (reorganizacao)
                EscreveLinhaListBox(listBoxInfoReorganizacao, "Cópia de segurança do banco de dados");
            else
                EscreveLinhaListBox(listBoxInfoRestauracao, "Cópia de segurança do banco de dados");


            using (ClassAguarde carregando = new ClassAguarde())
            {
                try
                {
                    resultado = RestauracaoDatabaseFirebird(@arquivoBackup);
                }
                catch (Exception ex)
                {
                    excecao = ex;
                }
            }

            if (resultado)
            {
                if (mostraMsg)
                    Mensagem.Aviso(this, "Restauração Realizada com sucesso !");
            }
            else
            {
                if (mostraMsg)
                    Mensagem.Excecao(this, "Erro ao realizar restore do banco.", excecao);
                else
                    throw new Exception(excecao.Message);
                refazCopiaSeg = true;
            }
        }

        private void restoreSvc_ServiceOutput(object sender, ServiceOutputEventArgs e)
        {
            if (reorganizacao)
                EscreveLinhaListBox(listBoxInfoReorganizacao, e.Message);
            else
                EscreveLinhaListBox(listBoxInfoRestauracao, e.Message);
        }

        private bool RestauracaoDatabaseFirebird(string arquivoBackup)
        {
            try
            {
                listBoxInfoRestauracao.Items.Clear();

                if (Conexao.OPEN)
                {
                    Conexao.Close();
                }

                Thread.Sleep(100);

                string_conexao = @"User=" + Conexao.USUARIO + ";Password=" + Conexao.SENHA + ";Database=" + bancoOriginal + ";DataSource=localhost;Port=3050;Dialect=3;";
                FbRestore restoreSvc = new FbRestore();
                restoreSvc.ServiceOutput += new ServiceOutputEventHandler(restoreSvc_ServiceOutput);
                restoreSvc.ConnectionString = string_conexao;
                restoreSvc.BackupFiles.Add(new FbBackupFile(arquivoBackup, 2048));
                restoreSvc.Verbose = true;
                restoreSvc.Options = FbRestoreFlags.Replace;
                restoreSvc.Execute();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Reorganização Firebird
        private void Reorganizacao()
        {
            string arquivoBackupTemp = Directory.GetCurrentDirectory() + @"\Temp\backup.gbk";

            EscreveLinhaListBox(listBoxInfoReorganizacao, "Realizando Backup");
            RealizandoBackup(@arquivoBackupTemp, false);
            EscreveLinhaListBox(listBoxInfoReorganizacao, "Realizando Restore");
            Thread.Sleep(500);
            RealizandoRestore(arquivoBackupTemp, false);

            Thread.Sleep(100);
            File.Delete(arquivoBackupTemp);
            File.Delete(bancoTemp);
        }

        private void buttonReorganizacao_Click(object sender, EventArgs e)
        {
            try
            {
                buttonRealizaReorganizacao.Enabled = false;
                bloqueiaFechamento = true;
                reorganizacao = true;
                Reorganizacao();
                realizouReorganizacao = true;
                Mensagem.Aviso(this, "Reorganização realizada com sucesso !!");
            }
            catch (Exception ex)
            {
                Mensagem.Excecao(this, "Erro ao tentar realizar a reorganização", ex);
            }

            buttonRealizaReorganizacao.Enabled = true;
            reorganizacao = false;
            bloqueiaFechamento = false;
        }
        #endregion

        #region Cópia Física
        private void buttonCopiaFisica_Click(object sender, EventArgs e)
        {
            if (textBoxArquivoCopiaFisica.Text == "")
            {
                Mensagem.Aviso(this, "É necessário selecionar o local do arquivo de cópia física para que seja possível realizar a cópia !");
                return;
            }
            
            bloqueiaFechamento = true;
            BloqueiaBotoesCopiaFisica();
            bgWCopia.RunWorkerAsync();
        }

        private void BloqueiaBotoesCopiaFisica()
        {
            buttonCopiaFisica.Enabled = !buttonCopiaFisica.Enabled;
            buttonSelecionaCopiaFisica.Enabled = !buttonSelecionaCopiaFisica.Enabled;
            textBoxArquivoCopiaFisica.Enabled = !textBoxArquivoCopiaFisica.Enabled;
            buttonCancelarCopia.Enabled = !buttonCancelarCopia.Enabled;
        }

        private void bgWCopia_DoWork(object sender, DoWorkEventArgs e)
        {
            XCopy(@bancoOriginal, @textBoxArquivoCopiaFisica.Text);
        }

        private void buttonCancelarCopia_Click(object sender, EventArgs e)
        {
            cancelouCopia = true;
        }

        void bgWCopia_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (cancelouCopia)
                Mensagem.Aviso(this, "Cópia Cancelada pelo usuário.");
            else
                Mensagem.Aviso(this, "Cópia física realizada com sucesso.");

            realizouCopiaFisica = true;
            progressBarProgresso.Value = 0;
            labelProgressoCopia.Text = "";
            BloqueiaBotoesCopiaFisica();
            bloqueiaFechamento = false;
        }

        private void buttonSelecionaCopiaFisica_Click(object sender, EventArgs e)
        {
            sFDialogCopiaFisica.FileName = "CF_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + Conexao.BANCO;
            if (sFDialogCopiaFisica.ShowDialog() == DialogResult.OK)
                textBoxArquivoCopiaFisica.Text = sFDialogCopiaFisica.FileName;
        }
        #endregion

        #region Cópia com progresso
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName,
           CopyProgressRoutine lpProgressRoutine, IntPtr lpData, ref Int32 pbCancel,
           CopyFileFlags dwCopyFlags);

        delegate CopyProgressResult CopyProgressRoutine(
        long TotalFileSize,
        long TotalBytesTransferred,
        long StreamSize,
        long StreamBytesTransferred,
        uint dwStreamNumber,
        CopyProgressCallbackReason dwCallbackReason,
        IntPtr hSourceFile,
        IntPtr hDestinationFile,
        IntPtr lpData);

        int pbCancel;

        enum CopyProgressResult : uint
        {
            PROGRESS_CONTINUE = 0,
            PROGRESS_CANCEL = 1,
            PROGRESS_STOP = 2,
            PROGRESS_QUIET = 3
        }

        enum CopyProgressCallbackReason : uint
        {
            CALLBACK_CHUNK_FINISHED = 0x00000000,
            CALLBACK_STREAM_SWITCH = 0x00000001
        }

        [Flags]
        enum CopyFileFlags : uint
        {
            COPY_FILE_FAIL_IF_EXISTS = 0x00000001,
            COPY_FILE_RESTARTABLE = 0x00000002,
            COPY_FILE_OPEN_SOURCE_FOR_WRITE = 0x00000004,
            COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 0x00000008
        }

        private void XCopy(string oldFile, string newFile)
        {
            CopyFileEx(oldFile, newFile, new CopyProgressRoutine(this.CopyProgressHandler), IntPtr.Zero, ref pbCancel, CopyFileFlags.COPY_FILE_RESTARTABLE);
        }

        private CopyProgressResult CopyProgressHandler(long total, long transferred, long streamSize, long StreamByteTrans, uint dwStreamNumber, CopyProgressCallbackReason reason, IntPtr hSourceFile, IntPtr hDestinationFile, IntPtr lpData)
        {
            long percent = (transferred * 100) / total;

            updateProgress(percent);
            Application.DoEvents();
            if (cancelouCopia)
                return CopyProgressResult.PROGRESS_CANCEL;
            else
                return CopyProgressResult.PROGRESS_CONTINUE;
        }

        private void updateProgress(long percent)
        {
            if (this.InvokeRequired)
            {
                // Se for necessário utilizar um invoke, o método é chamado novamente     
                BeginInvoke(new MethodInvoker(delegate() { updateProgress(percent); }));
            }
            else
            {
                progressBarProgresso.Value = Convert.ToInt32(percent);
                labelProgressoCopia.Text = "Copiando arquivo . . .                                                                 " + percent + "%";
                //this.Refresh(); 
                Application.DoEvents();
            }
        }

        #endregion

        public F_UTILITARIOBANCO()
        {
            InitializeComponent();
        }

        private void F_UTILITARIOBANCO_Load(object sender, EventArgs e)
        {
            using (ClassAguarde carregando = new ClassAguarde())
            {
                Conexao.CarregaConfXml();
                bancoOriginal = Conexao.SERVER + Conexao.BANCO;
            }
            FbConnection.ClearAllPools();
        }

        private void F_UTILITARIOBANCO_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bloqueiaFechamento)
            {
                e.Cancel = true;
                return;
            }

            if (refazCopiaSeg)
                RestauraCopiaSeguranca();

            AtualizaBancoDados();

            if ((Program.AplicacaoChamadora != null) &&
                (Program.AplicacaoChamadora != ""))
                ExecutarAplicacaoChamadora();
        }

        private void AtualizaBancoDados()
        {
            DateTime hoje = DateTime.Now;

            if (realizouBackup)
            {
                AtualizaTabelaParam("P_DT_ULTIMO_BACKUP", hoje);
            }

            if (realizouReorganizacao)
            {
                AtualizaTabelaParam("P_DT_ULTIMA_REGORGANIZACAO", hoje);
            }

            if (realizouCopiaFisica)
            {
                AtualizaTabelaParam("P_DT_ULTIMA_COPIA", hoje);
            }
        }

        private void AtualizaTabelaParam(string campo, DateTime data)
        {
            if (!Conexao.OPEN)
                Conexao.Conectar();

            string sql = "UPDATE PARAMETROS SET " + campo + "=@data";
            FbCommand command = new FbCommand(sql, (FbConnection)Conexao.CONN);
            command.Parameters.Add("data", FbDbType.Date);
            command.Parameters["data"].Value = data;
            command.ExecuteNonQuery();
        }

        private void ExecutarAplicacaoChamadora()
        {
            try
            {
                Process.Start(Program.AplicacaoChamadora);
            }
            catch (Exception ex)
            {
                Mensagem.Excecao(null, "Erro ao executar o aplicativo: " + Program.AplicacaoChamadora + " novamente.", ex);
            }
        }

        private void EscreveLinhaListBox(ListBox listBox, string texto)
        {
            listBox.Items.Add(texto);
            listBox.SelectedIndex = listBox.Items.Count - 1;
        }

        private void CopiaSeguranca()
        {
            File.Copy(bancoOriginal, @bancoTemp, true);
        }

        private void RestauraCopiaSeguranca()
        {
            try
            {
                File.Copy(@"\Temp\bd_temp.fdb", bancoOriginal, true);
            }
            catch (Exception ex)
            {
                Mensagem.Excecao(this, "Erro ao restaurar o banco de dados da aplicação.", ex);
            }
        }
    }
}

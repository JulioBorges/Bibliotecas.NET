using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using CustomControls.Fonte.Classes;

namespace Conection
{
    public partial class F_UTILITARIOBANCO : Form
    {
        readonly string diretorioIni = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Backup");

        bool bloqueiaFechamento, realizouBackup;

        #region Backup Firebird

        private void buttonSelecionarArquivoBackup_Click(object sender, EventArgs e)
        {
            sFDialogArquivoBackup.FileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + "." + sFDialogArquivoBackup.DefaultExt;
            sFDialogArquivoBackup.InitialDirectory = diretorioIni;

            if (sFDialogArquivoBackup.ShowDialog(this) == DialogResult.OK)
            {
                textBoxArquivoBackup.Text = sFDialogArquivoBackup.FileName;
            }
        }
        
        private void buttonRealizaBackup_Click(object sender, EventArgs e)
        {
            if (textBoxArquivoBackup.Text == String.Empty)
            {
                Mensagem.Aviso(this, "É necessário selecionar o local do arquivo de backup para que seja possível realizar o backup !");
                return;
            }

            BloqueiaControlesBackup();
            bloqueiaFechamento = true;
            RealizandoBackup(@textBoxArquivoBackup.Text);
            bloqueiaFechamento = false;
            BloqueiaControlesBackup();
        }

        private void BloqueiaControlesBackup()
        {
            textBoxArquivoBackup.Enabled = !textBoxArquivoBackup.Enabled;
            buttonSelecionarArquivoBackup.Enabled = !buttonSelecionarArquivoBackup.Enabled;
            buttonRealizaBackup.Enabled = !buttonRealizaBackup.Enabled;
        }

        private void RealizandoBackup(string arquivoBackup)
        {
            BackupSqlServer lbkBackup = new BackupSqlServer();
            lbkBackup.SetMetodoPercentagemBackup(i =>
                                                     {
                                                         SuspendLayout();
                                                         UpdateLabelProgresso(string.Format("{0} % concluído", i));
                                                         pgbProgresso.Value = i;
                                                         ResumeLayout(true);
                                                     });
            realizouBackup = lbkBackup.BackupDataBase(arquivoBackup);
        }

        #endregion

        public F_UTILITARIOBANCO()
        {
            InitializeComponent();
        }

        private void F_UTILITARIOBANCO_Load(object sender, EventArgs e)
        {
            Conexao.Close();
            if (!Directory.Exists(diretorioIni))
                Directory.CreateDirectory(diretorioIni);
        }

        private void F_UTILITARIOBANCO_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bloqueiaFechamento)
            {
                e.Cancel = true;
                return;
            }

            AtualizaBancoDados();
        }

        private void AtualizaBancoDados()
        {
            DateTime hoje = DateTime.Now;

            if (realizouBackup)
                AtualizaTabelaParam("P_DT_ULTIMO_BACKUP", hoje);
        }

        private static void AtualizaTabelaParam(string campo, DateTime data)
        {
            if (!Conexao.OPEN)
                Conexao.Conectar();

            string sql = "UPDATE PARAMETROS SET " + campo + "=@data";
            SqlCommand command = new SqlCommand(sql, (SqlConnection)Conexao.CONN);
            command.Parameters.AddWithValue("data", data.Date);
            try
            {
                command.ExecuteNonQuery();
            }
            catch { }
        }

        void UpdateLabelProgresso(String text)
        {
            if (lblProgresso.InvokeRequired)
            { lblProgresso.Invoke(new Action<String>(UpdateLabelProgresso), new object[] { text }); }
            else
            { lblProgresso.Text = text; 
            Application.DoEvents();}
        }
    }
}

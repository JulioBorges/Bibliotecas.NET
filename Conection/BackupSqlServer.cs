using System;
using System.Data.SqlClient;
using CustomControls.Fonte.Classes;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Conection
{
    public class BackupSqlServer
    {
        private readonly Restore pclsrestore = new Restore();

        private Action<int> MetodoPorcentagemBackup;

        private Action MetodoConclusaoBackup;

        public void SetMetodoPercentagemBackup(Action<int> actMetodoPorcentagemBackup)
        {
            MetodoPorcentagemBackup = actMetodoPorcentagemBackup;
        }

        public void SetMetodoBackupconcluido(Action actMetodoConclusaoBackup)
        {
            MetodoConclusaoBackup = actMetodoConclusaoBackup;
        }

        /// <summary>
        ///   Metodo responsavel pela realização da tarefa de backup
        /// </summary>
        /// <param name="strArquivoDestino"> Diretorio de destino do arquivo de backup </param>
        /// <returns> </returns>
        public bool BackupDataBase(String strArquivoDestino)
        {
            try
            {
                Server lclsServer = new Server(new ServerConnection((SqlConnection) Conexao.CONN));
                //Criando o objeto Backup
                Backup lbkpDatabase = new Backup
                                          {
                                              //Especificando que o tipo do backup é de um banco de dados
                                              Action = BackupActionType.Database,
                                              //Especificando uma descrição para o backup
                                              BackupSetDescription = "Backup completo do banco de dados",
                                              //Especificando o banco de dados do backup
                                              Database = Conexao.BANCO
                                          };

                //Setando que o tipo do backup é um arquivo
                BackupDeviceItem lbkpDevice = new BackupDeviceItem(strArquivoDestino, DeviceType.File);
                //Setando que os dispositivos associados à operação de backup serão inicializados como parte da operação
                lbkpDatabase.Initialize = true;
                //Setando que um checksum será calculado durante a operação de backup
                //Um checksum é uma verificação que checa a integridade de dados transmitidos através de um canal ou armazenados por um meio
                //Isto é feito calculando a soma de verificação dos dados antes do envio ou do armazenamento deles, e recalculá-los ao 
                //recebê-los ou recuperá-los do armazenamento. Se o valor obtido é o mesmo, as informações não sofreram alterações e portanto 
                //não estão corrompidas.
                lbkpDatabase.Checksum = true;
                //Setando que o backup deve continuar após um erro de checksum ser encontrado
                lbkpDatabase.ContinueAfterError = true;
                // Adiciona o dispositivo ao objeto backup
                lbkpDatabase.Devices.Add(lbkpDevice);
                //Setando a propriedade incremental para Falso para especificar que este é um backup completo do banco de dados
                lbkpDatabase.Incremental = false;
                //Setando que o log deve ser truncado após o término do backup
                lbkpDatabase.LogTruncation = BackupTruncateLogType.Truncate;
                lbkpDatabase.PercentComplete += backup_PercentComplete;
                lbkpDatabase.Complete += backup_Complete;
                // Perform backup.
                //Executando o backup completo
                lbkpDatabase.SqlBackup(lclsServer);

                //Realizando a validação do banco de dados
                bool lblnVerificaValidacao = Validacao(lclsServer, strArquivoDestino);
                if (!lblnVerificaValidacao)
                {
                    Mensagem.Erro(null,
                                  "Não foi possivel validar o banco de dados. Por favor, execute novamente!");
                    return false;
                }
                return true;
            }
            catch (Exception lex)
            {
                String lstrMensagemExcecao = "Não foi possivel realizar o Procedimento de backup!\nDetalhes :" +
                                             lex.Message;

                if (lex.InnerException != null)
                {
                    if (lex.InnerException.InnerException != null)
                    {
                        lstrMensagemExcecao += lex.InnerException.InnerException.Message;
                    }
                    else
                    {
                        lstrMensagemExcecao += lex.InnerException.Message;
                    }
                }

                Mensagem.Erro(null, lstrMensagemExcecao);
                return false;
            }
        }

        /// <summary>
        ///   Metodo responsavel pelo processo de validação do backup.
        /// </summary>
        /// <param name="srvServidor"> Nome do servidor </param>
        /// <param name="strDiretorioDestino"> Diretorio do arquivo de backup </param>
        /// <returns> </returns>
        public bool Validacao(Server srvServidor, String strDiretorioDestino)
        {
            //Após o backup, a validação do mesmo pode ser realizada usando o método SqlVerify. 
            //Como este é um método da classe Restore, é necessário criar uma instância desta classe.
            pclsrestore.Devices.AddDevice(strDiretorioDestino, DeviceType.File);
            pclsrestore.Database = Conexao.CONN.ConnectionString;
            bool lblnverificaValidacao = pclsrestore.SqlVerify(srvServidor);
            return lblnverificaValidacao;
        }

        #region Eventos

        //The event handlers
        private void backup_Complete
            (object sender, ServerMessageEventArgs e)
        {
            if (MetodoConclusaoBackup != null)
            {
                MetodoConclusaoBackup();
                return;
            }

            Mensagem.Aviso(null, "Backup concluído!");
        }

        private void backup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            if (MetodoPorcentagemBackup != null)
                MetodoPorcentagemBackup(e.Percent);
        }

        #endregion
    }
}
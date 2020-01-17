using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Services;
using MySql.Data.MySqlClient;
using System.IO;

namespace Conection
{
    /// <summary>
    /// ***********************************************///
    /// @criado por: Julio Cezar Borges                ///
    /// @data: 26/09/2011                              ///
    /// @motivo: Desenvolvimento do projeto            ///
    /// ***********************************************///
    /// @modificado por:                               ///
    /// @data:                                         ///
    /// @motivo:                                       ///
    /// ***********************************************///
    /// </summary>
    public static partial class Conexao
    {
        public static TIPO_SGDB tipoBanco;
        private static IDbTransaction transacao;

        public static void ChamaUtilitarioBanco()
        {
            new F_UTILITARIOBANCO().ShowDialog();
        }

        public static string SERVER { get; private set; }

        public static string USUARIO { get; private set; }

        public static string SENHA { get; private set; }

        public static string BANCO { get; private set; }

        public static bool OPEN
        {
            get
            {
                try
                {
                    if (CONN == null)
                        return false;
                    return CONN.State == ConnectionState.Open;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao verificar se a conexão esta aberta.\nDetalhes: " + ex.Message);
                }
            }
        }

        public static IDbConnection CONN { get; private set; }

        public static void Conectar()
        {
            try
            {
                //Padrão Singleton --> Somente uma conexão pode estar aberta para esta aplicação.
                if (CONN != null && CONN.State == ConnectionState.Open)
                    return;

                CarregaConfXml();

                switch (tipoBanco)
                {
                    case TIPO_SGDB.FIREBIRD:
                        {
                            CONN =
                                new FbConnection(@"User=" + USUARIO + ";Password=" + SENHA + ";Database=" + SERVER +
                                                 BANCO +
                                                 ";DataSource=localhost; Port=3050;Dialect=3; Charset=NONE;Role=;Connection lifetime=15;Pooling=true; MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=0;");
                            break;
                        }
                    case TIPO_SGDB.MSSQL:
                        {
                            string lstrConexao;
                            if (string.IsNullOrWhiteSpace(USUARIO))
                                lstrConexao = @"Data Source=" + SERVER + ";Initial Catalog=" + BANCO +
                                              ";Integrated Security=True;Pooling=False";
                            else
                            {
                                lstrConexao = @"Data Source=" + SERVER + ";Initial Catalog=" + BANCO +
                                              ";Persist Security Info=True;User ID=" + USUARIO;

                                if (!string.IsNullOrWhiteSpace(SENHA))
                                    lstrConexao += ";Password=" + SENHA;
                            }

                            CONN = new SqlConnection(lstrConexao);
                            break;
                        }
                    case TIPO_SGDB.ACCESS:
                        {
                            CONN =
                                new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + BANCO +
                                                    ";Persist Security Info=False;");
                            break;
                        }
                    case TIPO_SGDB.MYSQL:
                        {
                            if (SERVER == String.Empty)
                                SERVER = "localhost";
                            CONN =
                                new MySqlConnection(@"server=" + SERVER + ";User Id=" + USUARIO + ";password=" + SENHA +
                                                    ";Persist Security Info=True;database='" + BANCO +
                                                    "' providerName='MySql.Data.MySqlClient'");
                            break;
                        }
                }
                if (CONN != null)
                {
                    CONN.Open();
                }
                else
                {
                    throw new Exception("Arquivo de configuração está incorreto ou não existe.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar arquivo de configuração \nDetalhes: " + ex.Message);
            }
        }

        public static void CarregaConfXml()
        {
            var ds = new DataSet();
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Application.ExecutablePath));
            ds.ReadXml("conf.xml");

            string tipo = ds.Tables[0].Rows[0]["TIPOBANCO"].ToString();
            switch (tipo)
            {
                case "ACESS":
                    {
                        tipoBanco = TIPO_SGDB.ACCESS;
                        break;
                    }
                case "FIREBIRD":
                    {
                        tipoBanco = TIPO_SGDB.FIREBIRD;
                        break;
                    }
                case "MSSQL":
                    {
                        tipoBanco = TIPO_SGDB.MSSQL;
                        break;
                    }
                case "MYSQL":
                    {
                        tipoBanco = TIPO_SGDB.MYSQL;
                        break;
                    }
            }

            SERVER = ds.Tables[0].Rows[0]["SERVIDOR"].ToString();
            BANCO = ds.Tables[0].Rows[0]["BANCO"].ToString();
            USUARIO = ds.Tables[0].Rows[0]["USUARIO"].ToString();
            SENHA = ds.Tables[0].Rows[0]["SENHA"].ToString();

            /*
            if (!servidor.Substring(servidor.Length - 1, 1).Equals(@"\"))
            {
                servidor += @"\";
            }*/

            switch (tipoBanco)
            {
                case TIPO_SGDB.FIREBIRD:
                    if (!File.Exists(Path.Combine(SERVER, BANCO)))
                        throw new Exception("Arquivo de banco de dados não existe.\n" +
                                            "Favor verifique os dados do arquivo conf.xml que está no diretório raiz da aplicação.");
                    break;
            }
        }

        public static void BeginTransaction()
        {
            try
            {
                transacao = CONN.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao iniciar a transação. \nDetalhes: " + ex.Message);
            }
        }

        public static void Commit()
        {
            try
            {
                if (transacao != null)
                    transacao.Commit();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao comitar a transação.\nDetalhes: " + e.Message);
            }
        }

        public static void Rollback()
        {
            try
            {
                transacao.Rollback();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao realizar Rollback.\nDetalhes: " + ex.Message);
            }
        }

        public static bool Backup(string bkpFileName)
        {
            switch (tipoBanco)
            {
                case TIPO_SGDB.FIREBIRD:
                    {
                        return BackupFirebird(bkpFileName);
                    }
                case TIPO_SGDB.MSSQL:
                    {
                        return BackupMSSQL(bkpFileName);
                    }
                case TIPO_SGDB.ACCESS:
                    {
                        return false;
                    }
                case TIPO_SGDB.MYSQL:
                    {
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        private static bool BackupMSSQL(string bkpFileName)
        {
            return true;
        }

        public static bool Restore(string restFileName)
        {
            switch (tipoBanco)
            {
                case TIPO_SGDB.FIREBIRD:
                    {
                        return RestoreFirebird(restFileName);
                    }
                case TIPO_SGDB.MSSQL:
                    {
                        return false;
                    }
                case TIPO_SGDB.ACCESS:
                    {
                        return false;
                    }
                case TIPO_SGDB.MYSQL:
                    {
                        return false;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        private static bool BackupFirebird(string bkpFileName)
        {
            try
            {
                String string_conexao = @"User=SYSDBA;Password=masterkey;Database=" + SERVER + BANCO +
                                        ";DataSource=localhost;Port=3050;Dialect=3;";

                var backupSvc = new FbBackup
                                    {
                                        ConnectionString = string_conexao
                                    };

                backupSvc.BackupFiles.Add(new FbBackupFile(bkpFileName, 2048));
                backupSvc.Verbose = true;
                backupSvc.Options = FbBackupFlags.IgnoreLimbo;
                backupSvc.Execute();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao realizar o backup.\nDetalhes: " + ex.Message);
            }
        }

        private static bool RestoreFirebird(string restFileName)
        {
            try
            {
                if (CONN.State == ConnectionState.Open)
                    CONN.Close();

                Thread.Sleep(500);
                String string_conexao = @"User=SYSDBA;Password=jcsoftwares;Database=" + SERVER + BANCO +
                                        ";DataSource=localhost;Port=3050;Dialect=3;";

                var restoreSvc = new FbRestore
                                     {
                                         ConnectionString = string_conexao
                                     };
                restoreSvc.BackupFiles.Add(new FbBackupFile(restFileName, 2048));
                restoreSvc.Verbose = true;
                restoreSvc.Options = FbRestoreFlags.Replace;
                restoreSvc.Execute();
                CONN.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao realizar restore do banco.\nDetalhes: " + ex.Message);
            }
        }

        public static void Close()
        {
            try
            {
                CONN.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao fechar a conexão com o banco.\nDetalhes: " + ex.Message);
            }
        }

        public static object ExecuteScalar(string sql)
        {
            try
            {
                IDbCommand command = null;
                object obj = null;
                switch (tipoBanco)
                {
                    case TIPO_SGDB.FIREBIRD:
                        {
                            command = new FbCommand(sql, (FbConnection) CONN);
                            break;
                        }
                    case TIPO_SGDB.MSSQL:
                        {
                            command = new SqlCommand(sql, (SqlConnection) CONN);
                            break;
                        }
                    case TIPO_SGDB.ACCESS:
                        {
                            command = new OleDbCommand(sql, (OleDbConnection) CONN);
                            break;
                        }
                    case TIPO_SGDB.MYSQL:
                        {
                            command = new MySqlCommand(sql, (MySqlConnection) CONN);
                            break;
                        }
                }
                if (command != null)
                {
                    obj = command.ExecuteScalar();
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao executar o comando.\nDetalhes: " + ex.Message);
            }
        }

        public static bool ExecuteCommand(string sql)
        {
            try
            {
                IDbCommand command = null;

                switch (tipoBanco)
                {
                    case TIPO_SGDB.FIREBIRD:
                        {
                            command = new FbCommand(sql, (FbConnection) CONN);
                            break;
                        }
                    case TIPO_SGDB.MSSQL:
                        {
                            command = new SqlCommand(sql, (SqlConnection) CONN);
                            break;
                        }
                    case TIPO_SGDB.ACCESS:
                        {
                            command = new OleDbCommand(sql, (OleDbConnection) CONN);
                            break;
                        }
                    case TIPO_SGDB.MYSQL:
                        {
                            command = new MySqlCommand(sql, (MySqlConnection) CONN);
                            break;
                        }
                }

                if (command != null)
                    command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao executar o comando.\nDetalhes: " + ex.Message);
            }
        }

        public static IDbCommand GetCommand(string sql)
        {
            switch (tipoBanco)
            {
                default:
                    {
                        return new SqlCommand(sql, (SqlConnection)CONN);
                    }
                case TIPO_SGDB.FIREBIRD:
                    {
                        return new FbCommand(sql, (FbConnection)CONN);
                    }
                case TIPO_SGDB.ACCESS:
                    {
                        return new OleDbCommand(sql, (OleDbConnection)CONN);
                    }
                case TIPO_SGDB.MYSQL:
                    {
                        return new MySqlCommand(sql, (MySqlConnection)CONN);
                    }
            }
        }

        public static bool ExecuteCommandTransaction(string sql)
        {
            try
            {
                IDbCommand command = null;

                switch (tipoBanco)
                {
                    case TIPO_SGDB.FIREBIRD:
                        {
                            command = new FbCommand(sql, (FbConnection) CONN, (FbTransaction) transacao);
                            break;
                        }
                    case TIPO_SGDB.MSSQL:
                        {
                            command = new SqlCommand(sql, (SqlConnection) CONN, (SqlTransaction) transacao);
                            break;
                        }
                    case TIPO_SGDB.ACCESS:
                        {
                            command = new OleDbCommand(sql, (OleDbConnection) CONN, (OleDbTransaction) transacao);
                            break;
                        }
                    case TIPO_SGDB.MYSQL:
                        {
                            command = new MySqlCommand(sql, (MySqlConnection) CONN, (MySqlTransaction) transacao);
                            break;
                        }
                }

                if (command != null)
                    command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao executar o comando.\nDetalhes: " + ex.Message);
            }
        }

        public static IDbDataAdapter CarregaDados(DataSet ds, BindingSource bds, string sql)
        {
            try
            {
                IDbDataAdapter adapter = GetDataAdapter(sql);
                DbCommandBuilder commandBuilder = GetCommandBuilder();
                commandBuilder.DataAdapter = (DbDataAdapter) adapter;
                adapter.Fill(ds);
                bds.DataSource = ds;
                bds.DataMember = ds.Tables[0].TableName;
                return adapter;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar os dados.\nDetalhes: " + ex.Message);
            }
        }

        public static IDbDataAdapter CarregaDados(DataSet ds, BindingSource bds, string nomeTabela, string filtro)
        {
            try
            {
                string sql = "SELECT * FROM " + nomeTabela + " " + filtro;
                IDbDataAdapter adapter = GetDataAdapter(sql);
                DbCommandBuilder commandBuilder = GetCommandBuilder();
                commandBuilder.DataAdapter = (DbDataAdapter) adapter;
                adapter.Fill(ds);
                bds.DataSource = ds;
                bds.DataMember = ds.Tables[0].TableName;
                return adapter;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar os dados.\nDetalhes: " + ex.Message);
            }
        }

        public static IDbDataAdapter CarregaDados(DataSet dsDados, BindingSource bdsDados, string nomeTabela,
                                                  string campos, string filtro, string join)
        {
            try
            {
                string sql = "SELECT ";

                if (campos == String.Empty)
                    sql += "*";
                else
                    sql += campos;

                sql += " FROM " + nomeTabela + " " + join + " " + filtro;

                IDbDataAdapter adapter = GetDataAdapter(sql);
                DbCommandBuilder commandBuilder = GetCommandBuilder();
                commandBuilder.DataAdapter = (DbDataAdapter) adapter;
                adapter.Fill(dsDados);
                bdsDados.DataSource = dsDados;
                bdsDados.DataMember = dsDados.Tables[0].TableName;
                return adapter;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar os dados.\nDetalhes: " + ex.Message);
            }
        }

        public static IDbDataAdapter CarregaDados(DataSet dsDados, BindingSource bdsDados, string nomeTabela,
                                                  string filtro, string join, List<FieldDataGrid> colunasDataGridView,
                                                  DataGridView dgvDados, bool geraCamposSQL)
        {
            try
            {
                string sql = geraCamposSQL ? "SELECT " : "SELECT *";

                if (colunasDataGridView == null)
                    sql += "*";
                else
                {
                    bool first = true;

                    if (geraCamposSQL)
                    {
                        foreach (FieldDataGrid campoBD in colunasDataGridView)
                        {
                            if (first)
                            {
                                sql += campoBD.FieldName;
                                first = false;
                            }
                            else
                            {
                                sql += ", " + campoBD.FieldName;
                            }
                        }
                    }
                }

                sql += " FROM " + nomeTabela + " " + join + " " + filtro;

                IDbDataAdapter adapter = GetDataAdapter(sql);
                DbCommandBuilder commandBuilder = GetCommandBuilder();
                commandBuilder.DataAdapter = (DbDataAdapter) adapter;
                adapter.Fill(dsDados);
                bdsDados.DataSource = dsDados;
                bdsDados.DataMember = dsDados.Tables[0].TableName;

                dgvDados.DataSource = bdsDados;

                if (colunasDataGridView != null)
                {
                    dgvDados.Columns.Clear();
                    dgvDados.Update();
                    foreach (FieldDataGrid campoBD in colunasDataGridView)
                    {
                        dgvDados.Columns.Add(new DataGridViewTextBoxColumn
                                                 {
                                                     DataPropertyName = campoBD.FieldName,
                                                     HeaderText = campoBD.FieldAlias,
                                                     Name = campoBD.FieldName,
                                                     ReadOnly = true,
                                                     Width = campoBD.Visible ? campoBD.FieldWidth : -1,
                                                     Visible = campoBD.Visible
                                                 });
                    }
                    dgvDados.Update();
                }

                return adapter;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar os dados.\nDetalhes: " + ex.Message);
            }
        }

        private static IDbDataAdapter GetDataAdapter(string sql)
        {
            try
            {
                switch (tipoBanco)
                {
                    case TIPO_SGDB.FIREBIRD:
                        {
                            return new FbDataAdapter(sql, (FbConnection) CONN);
                        }
                    case TIPO_SGDB.MSSQL:
                        {
                            return new SqlDataAdapter(sql, (SqlConnection) CONN);
                        }
                    case TIPO_SGDB.ACCESS:
                        {
                            return new OleDbDataAdapter(sql, (OleDbConnection) CONN);
                        }
                    case TIPO_SGDB.MYSQL:
                        {
                            return new MySqlDataAdapter(sql, (MySqlConnection) CONN);
                        }
                    default:
                        {
                            return null;
                        }
                }
            }
            catch
            {
                return null;
            }
        }

        public static IDataReader ExecuteReader(string sql)
        {
            try
            {
                IDbCommand cmd = GetCommand();

                cmd.CommandText = sql;
                cmd.Connection = CONN;

                cmd.CommandType = CommandType.Text;
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro executar o SQL.\nDetalhes: " + ex.Message);
            }
        }

        private static DbCommandBuilder GetCommandBuilder()
        {
            try
            {
                switch (tipoBanco)
                {
                    case TIPO_SGDB.FIREBIRD:
                        {
                            return new FbCommandBuilder();
                        }
                    case TIPO_SGDB.MSSQL:
                        {
                            return new SqlCommandBuilder();
                        }
                    case TIPO_SGDB.ACCESS:
                        {
                            return new OleDbCommandBuilder();
                        }
                    case TIPO_SGDB.MYSQL:
                        {
                            return new MySqlCommandBuilder();
                        }
                    default:
                        {
                            return null;
                        }
                }
            }
            catch
            {
                return null;
            }
        }

        private static IDbCommand GetCommand()
        {
            try
            {
                switch (tipoBanco)
                {
                    case TIPO_SGDB.ACCESS:
                        {
                            return new OleDbCommand();
                        }
                    case TIPO_SGDB.FIREBIRD:
                        {
                            return new FbCommand();
                        }
                    case TIPO_SGDB.MSSQL:
                        {
                            return new SqlCommand();
                        }
                    case TIPO_SGDB.MYSQL:
                        {
                            return new MySqlCommand();
                        }
                    default:
                        {
                            return null;
                        }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
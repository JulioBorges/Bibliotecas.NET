using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using CustomControls.Fonte.Classes;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace Conection
{
    /// <summary>
    /// ***********************************************///
    /// @criado por: Julio Cezar Borges                ///
    /// @data: 27/09/2011                              ///
    /// @motivo: Desenvolvimento do projeto            ///
    /// ***********************************************///
    /// @modificado por:                               ///
    /// @data:                                         ///
    /// @motivo:                                       ///
    /// ***********************************************///
    /// </summary>
    public static partial class Conexao
    {

        public static DataSet GetDataSet(string sql)
        {
            try
            {
                IDbDataAdapter adapter = null;
                var ds = new DataSet();
                switch (tipoBanco)
                {
                    case TIPO_SGDB.FIREBIRD:
                        {
                            adapter = new FbDataAdapter(sql, (FbConnection)CONN);
                            break;
                        }
                    case TIPO_SGDB.MSSQL:
                        {
                            adapter = new SqlDataAdapter(sql, (SqlConnection)CONN);
                            break;
                        }
                    case TIPO_SGDB.ACCESS:
                        {
                            adapter = new OleDbDataAdapter(sql, (OleDbConnection)CONN);
                            break;
                        }
                    case TIPO_SGDB.MYSQL:
                        {
                            adapter = new MySqlDataAdapter(sql, (MySqlConnection)CONN);
                            break;
                        }
                }
                if (adapter != null)
                {
                    adapter.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o DataSet\nDetalhes: " + ex.Message);
            }
        }

        public static bool Post(IDbDataAdapter adapter, DataSet ds, BindingSource bds, string tabela, string campoId, DATAMODE dataMode, bool tabelaID_DateTime)
        {
            if (dataMode == DATAMODE.INSERT)
            {
                return Insert(adapter, ds, bds, tabela, campoId, tabelaID_DateTime);
            }
            return dataMode == DATAMODE.EDIT && Update(adapter, ds, bds);
        }

        public static bool Insert(IDbDataAdapter adapter, DataSet ds, BindingSource bds, string tabela, string campoId, bool tabelaID_DateTime)
        {
            try
            {
                bds.Position = bds.Count - 1;
                if (tabelaID_DateTime)
                {
                    if (((DataRowView)bds.Current).Row[campoId] == DBNull.Value)
                        ((DataRowView)bds.Current).Row[campoId] = DateTime.Now;
                }
                else
                    ((DataRowView)bds.Current).Row[campoId] = GetID(tabela, campoId);

                bds.EndEdit();
                adapter.Update(ds);
                ds.Clear();
                adapter.Fill(ds);
                bds.DataSource = ds;
                bds.DataMember = ds.Tables[0].TableName;
                bds.Position = bds.Count - 1;
                return true;
            }
            catch (Exception ex)
            {
                Mensagem.Excecao(null, "Erro ao inserir os dados.", ex);
                return false;
            }
        }

        private static bool Update(IDataAdapter adapter, DataSet ds, BindingSource bds)
        {
            try
            {
                bds.EndEdit();
                adapter.Update(ds);
                return true;
            }
            catch (Exception ex)
            {
                Mensagem.Excecao(null, "Erro ao atualizar os dados.", ex);
                return false;
            }
        }

        public static bool Delete(IDbDataAdapter adapter, DataSet ds, BindingSource bds)
        {
            try
            {
                if (Mensagem.Pergunta(null, "Deseja realmente deletar o registro corrente?", DialogResult.No))
                {
                    return false;
                }

                bds.RemoveCurrent();
                bds.EndEdit();
                adapter.Update(ds);
                ds.Clear();
                adapter.Fill(ds);
                bds.DataSource = ds;
                bds.DataMember = ds.Tables[0].TableName;
                Commit();
                return true;
            }
            catch (Exception ex)
            {
                Mensagem.Excecao(null, "Erro ao deletar o registro corrente.", ex);
                RefreshData(adapter, ds, bds);
                return false;
            }
        }

        public static void Edit(object[] controlesEdit, bool habilitar)
        {
            //Este métodos configura os controles dos formulários para o modo de edição.
            //Caso o atributo habilitar esteja true, quer dizer que a edição de dados está sendo iniciada.
            try
            {
                foreach (object obj in controlesEdit)
                {
                    if (obj is Control)
                    {
                        ((Control)obj).Enabled = habilitar;
                    }
                    else if (obj is DataGridViewColumn)
                    {
                        ((DataGridViewColumn)obj).ReadOnly = !habilitar;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagem.Excecao(null, "Erro ao editar os dados.", ex);
            }
        }

        public static int GetID(string tableName, string campoId)
        {
            //Método que retorna o código de identificação de uma tabela
            try
            {
                IDbCommand cmd = GetCommand();
                string cmdSql = "SELECT MAX(" + campoId + ") + 1 AS NEW FROM " + tableName.ToUpper();

                cmd.CommandText = cmdSql;
                cmd.Connection = CONN;

                cmd.CommandType = CommandType.Text;
                string id = cmd.ExecuteScalar().ToString();
                
                if (id == String.Empty)
                    id = "1";

                cmd.Dispose();
                return Convert.ToInt32(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar o código da tabela " + tableName + ".\nDetalhes: " + ex.Message);
            }
        }

        public static void RefreshData(IDbDataAdapter adapter, DataSet ds, BindingSource bds)
        {
            try
            {
                int posicao = bds.Position;
                ds.Clear();
                adapter.Fill(ds);
                bds.DataSource = ds;
                bds.DataMember = ds.Tables[0].TableName;
                bds.Position = posicao;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar os dados.\nDetalhes: " + ex.Message);
            }
        }

        public static Image ImageFromDatabase(BindingSource bds, string campo)
        {
            Object obj = ((DataRowView)bds.Current)[campo];

            if (obj != DBNull.Value)
                return Image.FromStream(new MemoryStream((byte[])obj));
            return null;
        }

        public static void GravaFotoBD(IDbDataAdapter adpt, DataSet ds, BindingSource bds, string tabela, string campoFoto, PictureBox pctBox)
        {
            try
            {
                var Dados = (DataRowView)bds.Current;

                var ms = new MemoryStream();
                pctBox.Image.Save(ms, ImageFormat.Jpeg);
                var arrayFoto = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arrayFoto, 0, arrayFoto.Length);
                Dados[campoFoto] = arrayFoto;

                Post(adpt, ds, bds, tabela, String.Empty, DATAMODE.EDIT, false);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gravar a imagem no banco.\nDetalhes: " + ex.Message);
            }
        }

        public static void CarregaComboboxBancoDados(this ComboBox cbDados, string nomeTabela, string filtro, string displayMember, string valueMember)
        {
            var ds = new DataSet(nomeTabela);
            var bds = new BindingSource();
            CarregaDados(ds, bds, nomeTabela, filtro);
            cbDados.DataSource = ds.Tables[0];
            cbDados.ValueMember = valueMember;
            cbDados.DisplayMember = displayMember;
        }

        public static void AddNew(ToolStripButton[] botoesBrowse, ToolStripButton[] botoesEdit, object[] controlesEdit, DataRowView row, string tableName, string campoId, bool tabelaID_DateTime)
        {
            if (botoesBrowse != null)
                Funcoes.HabilitaBotoesPesquisa(botoesBrowse, botoesEdit, false);

            if (controlesEdit != null)
                Edit(controlesEdit, true);

            //Caso o padrão de inserção da empresa seja de apresentar o código antes de efetivar a inserção do registro,
            //então esta linha deve ser descomentada e a linha de inserção do código no momento da gravação deve ser comentada.
            //Usando esta exibição do código antes de gravar pode fazer com que, ao se cancelar uma inserção, o código que foi
            //gerado seja perdido.
            if (tabelaID_DateTime)
                row[campoId] = DateTime.Now;
            else
                row[campoId] = GetID(tableName, campoId);
        }

        public static void Edit(BindingSource bds, ToolStripButton[] botoesBrowse, ToolStripButton[] botoesEdit, object[] controlesEdit)
        {
            if (bds.Count > 0)
            {
                Funcoes.HabilitaBotoesPesquisa(botoesBrowse, botoesEdit, false);
                Edit(controlesEdit, true);
            }
        }

        public static void Post(ToolStripButton[] botoesBrowse, ToolStripButton[] botoesEdit, object[] controlesEdit)
        {
            Funcoes.HabilitaBotoesPesquisa(botoesBrowse, botoesEdit, true);
            Edit(controlesEdit, false);
        }

        public static void Cancel(BindingSource bds, ToolStripButton[] botoesBrowse, ToolStripButton[] botoesEdit, object[] controlesEdit)
        {
            bds.CancelEdit();
            Edit(controlesEdit, false);
            Funcoes.HabilitaBotoesPesquisa(botoesBrowse, botoesEdit, true);
        }
    }
}

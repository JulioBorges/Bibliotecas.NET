using System.Data;

namespace Conection.MigracaoBancoDados.Abstract
{
    public abstract class AMigracaoBancoDados
    {
        protected readonly int pintCodigoCompatibilidade;

        public int VersaoBancoDados
        {
            get
            {
                return pintCodigoCompatibilidade;
            }
        }

        protected AMigracaoBancoDados(int intCodigoCompatibilidade)
        {
            pintCodigoCompatibilidade = intCodigoCompatibilidade;
        }

        public abstract void Up();

        public abstract void Down();

        public void AtualizarVersaoBanco()
        {
            Sql("UPDATE PARAMETROS SET P_CD_VERSAO = " + pintCodigoCompatibilidade);
        }

        public void Sql(string strComando)
        {
            Conexao.Conectar();
            IDbCommand lcmdComando = Conexao.GetCommand(strComando);
            lcmdComando.ExecuteNonQuery();
            Conexao.Close();
        }

        public void AddColumn(string strNomeTabela, string strNomeCampo,
            string strTipoDados, bool blnNotNull = false, string strDefault = "")
        {
            Conexao.Conectar();

            string lstrComando = string.Format("ALTER TABLE {0} ADD {1} {2} {3}",
                strNomeTabela, strNomeCampo, strTipoDados, blnNotNull ? "NOT NULL DEFAULT " + strDefault : "");
            IDbCommand lcmdComando = Conexao.GetCommand(lstrComando);
            lcmdComando.ExecuteNonQuery();
            Conexao.Close();
        }

        public void DropColumn(string strNomeTabela, string strNomeCampo)
        {
            Conexao.Conectar();

            string lstrComando = string.Format("ALTER TABLE {0} DROP {1}",
                strNomeTabela, strNomeCampo);
            IDbCommand lcmdComando = Conexao.GetCommand(lstrComando);
            lcmdComando.ExecuteNonQuery();
            Conexao.Close();
        }
    }
}

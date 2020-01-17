using System.Collections.Generic;
using System.Data;
using System.Linq;
using Conection.MigracaoBancoDados.Abstract;

namespace Conection.MigracaoBancoDados
{
    public class AtualizarBanco
    {
        public void VerificarAtualizacaoVersao()
        {
            string lstrComando =
                "SELECT column_name from INFORMATION_SCHEMA.columns " +
                "   where table_name = 'PARAMETROS' and " +
                "         column_name = 'P_CD_VERSAO'";

            Conexao.Conectar();
            IDbCommand lcmd = Conexao.GetCommand(lstrComando);

            var lresult = lcmd.ExecuteScalar();

            // se não tiver o campo, então significa que que o banco nao esta preparado para atualização automatica.
            if (lresult == null || lresult.ToString().Equals(""))
            {
                IDbCommand lcmdUpdate = Conexao.GetCommand("ALTER TABLE PARAMETROS ADD P_CD_VERSAO INT");
                lcmdUpdate.ExecuteNonQuery();

                lcmdUpdate = Conexao.GetCommand("UPDATE PARAMETROS SET P_CD_VERSAO = 1");
                lcmdUpdate.ExecuteNonQuery();
            }
            Conexao.Close();
        }

        public void AtualizarVersao(List<AMigracaoBancoDados> lstUpdates, int intVersaoAtual)
        {
            Conexao.Conectar();
            var llstUpdatesAtualizados = lstUpdates.Where(o => o.VersaoBancoDados > intVersaoAtual)
                .OrderBy(o => o.VersaoBancoDados);

            foreach (AMigracaoBancoDados lclsUpdate in llstUpdatesAtualizados)
            {
                lclsUpdate.Up();
                lclsUpdate.AtualizarVersaoBanco();
            }

            Conexao.Close();
        }
    }
}
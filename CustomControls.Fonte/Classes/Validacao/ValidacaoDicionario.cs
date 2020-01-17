//           *-----------------------------------------------------------------*
//           *                                                                 *
//           *    CRIADO POR...: Julio C. Borges.                              *
//           *    DATA.........: 24/06/2013.                                   *
//           *    MOTIVO.......:                                               *
//           *                                                                 *
//           *-----------------------------------------------------------------*

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CustomControls.Fonte.Classes.Validacao
{
    /// <summary>
    /// Classe responsavel pela validação e alimentação de quebra de regras (Valores maiores que o permitido) ocorridas no aplicativo
    /// </summary>
    public class ValidacaoDicionario
    {
        #region Atributos

        /// <summary>
        /// Dictionary com a lista de erros para validação
        /// </summary>
        private readonly IDictionary<string, string> pdicValidacaoDicionario = new Dictionary<string, string>();

        #endregion

        #region Métodos públicos

        #region Validação de campos

        /// <summary>
        /// Realiza a validação de campos inteiros Not Null.
        /// </summary>
        /// <param name="strChave">Informação de chave para o dicionário</param>
        /// <param name="strDescricaoCampo">Descrição amigável do campo </param>
        /// <param name="intValor">Valor a ser validado</param>
        public void ValidarCampoInteiroNotNull(string strChave, string strDescricaoCampo, int intValor)
        {
            if (intValor <= 0)
                AdicionarErroCampoNaoInformado(strChave, strDescricaoCampo);
        }

        /// <summary>
        /// Realiza a validação de um campo string Not Null e com tamanho máximo.
        /// </summary>
        /// <param name="strChave">Informação de chave para o dicionário</param>
        /// <param name="strDescricaoCampo">Descrição amigável do campo </param>
        /// <param name="strValor">Valor a ser validado</param>
        /// <param name="intTamanhoMaximo">Tamanho máximo da string</param>
        public void ValidarCampoStringNotNullMaxLength(string strChave, string strDescricaoCampo, string strValor, int intTamanhoMaximo)
        {
            if (string.IsNullOrEmpty(strValor))
                AdicionarErroCampoNaoInformado(strChave, strDescricaoCampo);
            else if (strValor.Length > intTamanhoMaximo)
                AdicionarErroTamanhoCampo(strChave, strDescricaoCampo, intTamanhoMaximo);
        }

        /// <summary>
        /// Realiza a validação do tamanho máximo de um campo string.
        /// </summary>
        /// <param name="strChave">Informação de chave para o dicionário</param>
        /// <param name="strDescricaoCampo">Descrição amigável do campo </param>
        /// <param name="strValor">Valor a ser validado</param>
        /// <param name="intTamanhoMaximo">Tamanho máximo da string</param>
        public void ValidarCampoStringMaxLength(string strChave, string strDescricaoCampo, string strValor, int intTamanhoMaximo)
        {
            if (!string.IsNullOrEmpty(strValor) && strValor.Length > intTamanhoMaximo)
                AdicionarErroTamanhoCampo(strChave, strDescricaoCampo, intTamanhoMaximo);
        }

        /// <summary>
        /// Realiza a validação de um campo string que representa um diretório.
        /// </summary>
        /// <param name="strChave">Informação de chave para o dicionário</param>
        /// <param name="strDescricaoCampo">Descrição amigável do campo </param>
        /// <param name="strValor">Valor a ser validado</param>
        /// <param name="intTamanhoMaximo">Tamanho máximo da string</param>
        public void ValidarCampoStringDiretorioNotNull(string strChave, string strDescricaoCampo, string strValor, int intTamanhoMaximo)
        {
            ValidarCampoStringNotNullMaxLength(strChave, strDescricaoCampo, strValor, intTamanhoMaximo);

            if (string.IsNullOrEmpty(strValor))
                return;

            if (Directory.Exists(strValor))
                AdicionarErro(strChave, string.Format("{0} inexistente.", strDescricaoCampo));
        }

        #endregion

        /// <summary>
        /// Metodo que adiciona em um dicionario o erro ocorrido.
        /// </summary>
        /// <param name="strChave">Informação de chave para o dicionario</param>
        /// <param name="strValor">Descrição do erro para o dicionario</param>
        public void AdicionarErro(string strChave, string strValor)
        {
            if (!pdicValidacaoDicionario.ContainsKey(strChave))
                pdicValidacaoDicionario.Add(strChave, strValor);
        }

        /// <summary>
        /// Metodo que limpa dicionario contendo os erros ocorridos.
        /// </summary>
        public void LimparErros()
        {
            pdicValidacaoDicionario.Clear();
        }

        /// <summary>
        /// Metodo responsavel por retornar se houve ou não validação do dados.
        /// </summary>
        /// <returns></returns>
        public bool ValidacaoExito()
        {
            return !pdicValidacaoDicionario.Any();
        }

        /// <summary>
        /// Retorna a Lista de Erros 
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> RetornaListaErros()
        {
            return pdicValidacaoDicionario;
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Metodo que adiciona em um dicionario o erro de tamanho de campo inválido, 
        /// informando o camop e o tamanho máximo
        /// </summary>
        /// <param name="strChave">Informação de chave para o dicionário</param>
        /// <param name="strDescricaoCampo">Descrição amigável do campo </param>
        /// <param name="intTamanhoMaximo">tamanho máximo do campo</param>
        private void AdicionarErroTamanhoCampo(string strChave, string strDescricaoCampo, int intTamanhoMaximo)
        {
            AdicionarErro(strChave, string.Format("O campo \"{0}\" deve conter no máximo {1} caracteres.", strDescricaoCampo, intTamanhoMaximo));
        }

        /// <summary>
        /// Metodo que adiciona em um dicionario o erro de campo não informado, 
        /// informando o camop e o tamanho máximo
        /// </summary>
        /// <param name="strChave">Informação de chave para o dicionário</param>
        /// <param name="strDescricaoCampo">Descrição amigável do campo </param>
        private void AdicionarErroCampoNaoInformado(string strChave, string strDescricaoCampo)
        {
            AdicionarErro(strChave, string.Format("O campo \"{0}\" deve ser informado.", strDescricaoCampo));
        }

        #endregion
    }
}
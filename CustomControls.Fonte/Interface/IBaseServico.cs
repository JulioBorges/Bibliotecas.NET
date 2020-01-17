//           *-----------------------------------------------------------------*
//           *                                                                 *
//           *    CRIADO POR...: Julio C. Borges.                              *
//           *    DATA.........: 24/06/2013.                                   *
//           *    MOTIVO.......:                                               *
//           *                                                                 *
//           *-----------------------------------------------------------------*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CustomControls.Fonte.Classes.Validacao;

namespace CustomControls.Fonte.Interface
{
    /// <summary>
    /// Interface base.
    /// </summary>
    /// <typeparam name="T">Entidade a ser trabalhada</typeparam>
    /// <typeparam name="TRep">Classe do repositório</typeparam>
    public abstract class IBaseServico<T, TRep> : IDisposable
        where T : class
        where TRep : IRepositorioGenerico<T>, new()
    {
        #region Atributos

        protected TRep giRepositorio;

        protected ValidacaoDicionario pclsValidacaoDicionario = new ValidacaoDicionario();

        #endregion

        #region Propriedades

        /// <summary>
        /// Retorna a Classe de Validacao
        /// </summary>
        public ValidacaoDicionario gclsValidacaoDicionario
        {
            get { return pclsValidacaoDicionario; }
            set { pclsValidacaoDicionario = value; }
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Construtor padrão
        /// </summary>
        protected IBaseServico()
            : this(new TRep())
        {
        }

        /// <summary>
        /// Construtor com repasse da interface repositorio de Arquivo
        /// </summary>
        /// <param name="iArquivo_Repositorio">Interface repositorio do Arquivo</param>
        protected IBaseServico(TRep iArquivo_Repositorio)
        {
            giRepositorio = iArquivo_Repositorio;
        }

        /// <summary>
        /// Construtor com os parâmetros de validação e interface do repositório
        /// </summary>
        /// <param name="clsValidacaoDicionario">Classe validação de dicionário de erros</param>
        /// <param name="iRepositorio">Interface do repositório</param>
        protected IBaseServico(ValidacaoDicionario clsValidacaoDicionario, TRep iRepositorio)
        {
            pclsValidacaoDicionario = clsValidacaoDicionario;
            giRepositorio = iRepositorio;
        }

        /// <summary>
        /// Construtor com parâmetro de validação de dados.
        /// </summary>
        /// <param name="clsValidacaoDicionario">Classe de dicionario de validação</param>
        protected IBaseServico(ValidacaoDicionario clsValidacaoDicionario) :
            this(clsValidacaoDicionario, new TRep())
        {
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Listar todos registros
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> ListarTodos()
        {
            return giRepositorio.ListarTodos();
        }

        /// <summary>
        /// Pesquisar Registro
        /// </summary>
        /// <param name="expPredicado">Expressão a ser realizada no processo de pesquisa</param>
        /// <returns></returns>
        public virtual IQueryable<T> Pesquisar(Expression<Func<T, bool>> expPredicado)
        {
            return giRepositorio.Pesquisar(expPredicado);
        }

        /// <summary>
        /// Pesquisar por codigo
        /// </summary>
        /// <param name="intCodigo">Codigo a ser informado</param>
        /// <returns></returns>
        public virtual T PesquisarPorCodigo(int intCodigo)
        {
            return giRepositorio.PesquisarPorCodigo(intCodigo);
        }

        /// <summary>
        /// Adicionar registro
        /// </summary>
        /// <param name="clsT">Entidade contendo as informações</param>
        /// <returns></returns>
        public virtual bool Adicionar(T clsT)
        {
            return giRepositorio.Adicionar(clsT);
        }

        /// <summary>
        /// Excluir registro
        /// </summary>
        /// <param name="clsT">Entidade contendo as informações</param>
        /// <returns></returns>
        public virtual bool Excluir(T clsT)
        {
            return giRepositorio.Excluir(clsT);
        }

        /// <summary>
        /// Editar registro
        /// </summary>
        /// <param name="clsT">Entidade contendo as informações</param>
        /// <returns></returns>
        public virtual bool Editar(T clsT)
        {
            return giRepositorio.Editar(clsT);
        }

        /// <summary>
        /// Gravar registro
        /// </summary>
        /// <param name="clsT">Entidade contendo as informações</param>
        /// <returns></returns>
        public virtual bool Gravar(T clsT)
        {
            return Adicionar(clsT) && giRepositorio.Salvar(clsT);
        }

        /// <summary>
        /// Descartar Alteracoes
        /// </summary>
        public virtual bool DescartarAlteracoes(T clsT)
        {
            return giRepositorio.DescartarAlteracoes(clsT);
        }

        #region Validação

        /// <summary>
        /// Validação dos atributos da Entidade
        /// </summary>
        /// <param name="clsT">Entidade a ser Validada</param>
        /// <returns></returns>
        public abstract bool Validar(T clsT);

        #endregion

        /// <summary>
        /// Metodo para retornar o proximo codigo.
        /// </summary>
        /// <returns></returns>
        public virtual int RetornarProximoCodigo()
        {
            // Utiliza reflection para verificar se generalizar o método.
            // Como a maioria das tabelas possui o campo código este método
            // passou a ser criado dentro do IBaseServico, ao invés de ficar nas 
            // classes específicas. Se tentar usar o método para uma entidade 
            // que não possui o campo código o sistema irá lançar uma exceção específica.
            // Julio - 04/06/2013

            var llstItens = ListarTodos();

            if (llstItens.Count == 0)
                return 1;

            return llstItens.Max(o => (int)RecuperarValorPropriedade(o, "Codigo")) + 1;
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Verifica a existencia de um determinado campo na entidade, 
        /// lançando uma exceção NotSupportedException caso o mesmo 
        /// não exista
        /// </summary>
        /// <param name="objEntidade">Entidade a ser verificada</param>
        /// <param name="strNomePropriedade">Nome da propriedade a ser verificada</param>
        protected static void VerificaExistenciaPropriedadeEntidade(object objEntidade, string strNomePropriedade)
        {
            if (objEntidade.GetType().GetProperties().All(o => o.Name != strNomePropriedade))
                throw new NotSupportedException(string.Format("Entidade não possui campo {0} !", strNomePropriedade));
        }

        /// <summary>
        /// Recupera o valor de uma determinada propriedade via reflection.
        /// </summary>
        /// <param name="objInstanciaObjeto">Objeto a ser verificado</param>
        /// <param name="strNomePropriedade">Propriedade a ser verificada</param>
        /// <returns></returns>
        protected static object RecuperarValorPropriedade(object objInstanciaObjeto, string strNomePropriedade)
        {
            VerificaExistenciaPropriedadeEntidade(objInstanciaObjeto, strNomePropriedade);
            return objInstanciaObjeto.GetType().GetProperty(strNomePropriedade).GetValue(objInstanciaObjeto, null);
        }

        #endregion

        #region Métodos de IDisposable

        public void Dispose()
        {
            giRepositorio.Dispose();
        }

        #endregion
    }
}
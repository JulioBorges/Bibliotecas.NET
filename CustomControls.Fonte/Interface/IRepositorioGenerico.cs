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

namespace CustomControls.Fonte.Interface
{
    /// <sumary>
    ///
    /// </sumary>
    public interface IRepositorioGenerico <T> where T : class
    {
        /// <summary>
        /// Listar todos
        /// </summary>
        /// <returns></returns>
        IList<T> ListarTodos();

        /// <summary>
        /// Pesquisar por expressão
        /// </summary>
        /// <param name="predicate">Expressão de pesquisa</param>
        /// <returns></returns>
        IQueryable<T> Pesquisar(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Pesquisar por codigo
        /// </summary>
        /// <param name="intCodigo">Codigo a ser pesquisado</param>
        /// <returns></returns>
        T PesquisarPorCodigo(int intCodigo);

        /// <summary>
        /// Processo de exclusão
        /// </summary>
        /// <param name="entity">Entidade a ser processada</param>
        bool Excluir(T entity);

        /// <summary>
        /// Processo de exclusão
        /// </summary>
        /// <param name="listaEntity">Lista a ser processada</param>
        bool ExcluirLista(IEnumerable<T> listaEntity);

        /// <summary>
        /// Processo de Gravação
        /// </summary>
        /// <param name="listaEntity">Lista a ser processada</param>
        bool GravarLista(IEnumerable<T> listaEntity);

        /// <summary>
        /// Processo de Edição de Lista
        /// </summary>
        /// <param name="listaEntity">Lista a ser processada</param>
        bool EditarLista(IEnumerable<T> listaEntity);

        /// <summary>
        /// Retornar proximo codigo.
        /// </summary>
        /// <returns></returns>
        int RetornarProximoCodigo();

        /// <summary>
        /// Processo de edição
        /// </summary>
        /// <param name="entity">Entidade a ser processada</param>
        bool Editar(T entity);

        /// <summary>
        /// Processo de gravação
        /// </summary>
        /// <param name="entity">Entidade a ser processada</param>
        bool Salvar(T entity);

        /// <summary>
        /// Processo de Adição
        /// </summary>
        /// <param name="entity">Entidade a ser processada</param>
        bool Adicionar(T entity);

        /// <summary>
        /// Processo de Descarregamento
        /// </summary>
        void Dispose();

        /// <summary>
        /// Descartar alterações
        /// </summary>
        bool DescartarAlteracoes(T entity);  
    }
}
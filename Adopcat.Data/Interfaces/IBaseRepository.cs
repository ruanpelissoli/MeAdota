using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Adopcat.Data.Interfaces
{
    /// <summary>
    /// Interface para manipulação de dados do banco.
    /// </summary>
    /// <typeparam name="T">Entidade que será manipulada.</typeparam>
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Recupera todos os registros do banco de dados.
        /// </summary>
        /// <returns>A lista com os registros encontrados.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Recupera todos os registros do banco de dados utilizando filtros.
        /// </summary>
        /// <param name="filterExpression">Expressão de filtro</param>
        /// <returns>A lista com os registros encontrados.</returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> filterExpression);

        /// <summary>
        /// Retorna todos os registros do banco de dados utilizando paginação e filtros asincrono.
        /// </summary>
        /// <param name="filterExpression">Expressão de filtro</param>
        /// <param name="index">Índice inicial</param>
        /// <param name="size">Tamanho da página</param>
        /// <param name="total">Contagem total de registros</param>
        /// <returns>A lista com os registros encontrados.</returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> filterExpression, out int total, int index = 0, int size = 50);

        /// <summary>
        /// Recupera todos os registros do banco de dados.
        /// </summary>
        /// <returns>A lista com os registros encontrados.</returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Recupera todos os registros do banco de dados utilizando filtros asincrono.
        /// </summary>
        /// <param name="filterExpression">Expressão de filtro</param>
        /// <returns>A lista com os registros encontrados.</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filterExpression);

        /// <summary>
        /// Retorna todos os registros do banco de dados utilizando paginação e filtros asincrono.
        /// </summary>
        /// <param name="filterExpression">Expressão de filtro</param>
        /// <param name="index">Índice inicial</param>
        /// <param name="size">Tamanho da página</param>
        /// <param name="total">Contagem total de registros</param>
        /// <returns>A lista com os registros encontrados.</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filterExpression, out int total, int index = 0, int size = 50);

        /// <summary>
        /// Verifica se existem registros para determinado filtro.
        /// </summary>
        /// <param name="filterExpression">Expressão de filtro</param>
        /// <returns>TRUE se encontrou registros.</returns>
        bool Contains(Expression<Func<T, bool>> filterExpression);

        /// <summary>
        /// Busca um registro do banco de dados, através de um filtro.
        /// </summary>
        /// <param name="filterExpression">Expressão de filtro</param>
        /// <returns>O primeiro registro encontrado.</returns>
        Task<T> FindAsync(Expression<Func<T, bool>> filterExpression);

        /// <summary>
        /// Cria registros no banco de dados através de uma lista.
        /// </summary>
        /// <param name="t">A lista de registros a serem criados.</param>
        /// <returns>A lista de registros criados.</returns>
        Task<IQueryable<T>> CreateAsync(IQueryable<T> t);

        /// <summary>
        /// Cria um registro no banco de dados.
        /// </summary>
        /// <param name="t">O registro a ser criado.</param>
        /// <returns>O registro criado.</returns>
        Task<T> CreateAsync(T t);

        /// <summary>
        /// Exclui um registro do banco de dados.
        /// </summary>
        /// <param name="t">O registro a ser excluído.</param>
        /// <returns>A quantidade de linhas afetadas.</returns>
        Task<int> DeleteAsync(T t);

        /// <summary>
        /// Exclui registros do banco de dados de acordo com um filtro.
        /// </summary>
        /// <param name="filterExpression">Expressão de filtro.</param>
        /// <returns>A quantidade de registros afetados.</returns>
        Task<int> DeleteAsync(Expression<Func<T, bool>> filterExpression);

        /// <summary>
        /// Atualiza um registro no banco de dados.
        /// </summary>
        /// <param name="t">O registro a ser atualizado.</param>
        /// <returns>O registro atualizado.</returns>
        Task<int> UpdateAsync(T t);

        /// <summary>
        /// Retorna a quantidade de registros no banco.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Retorna a quantidade de registros no banco através de um filtro.
        /// </summary>
        /// <param name="filterExpression">Expressão de filtro.</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> filterExpression);
    }
}

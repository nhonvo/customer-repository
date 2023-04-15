using System.Linq.Expressions;
using Application.Commons;

namespace Application.Repositories
{
    public interface IGenericRepository<T> where T : class
    {

        /// <summary>
        /// Adds a new entity of type T to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Adds a collection of entities of type T to the database.
        /// </summary>
        /// <param name="entity">The collection of entities to add.</param>
        Task AddRangeAsync(IEnumerable<T> entity);

        /// <summary>
        /// Checks if any entity of type T satisfies the given filter.
        /// </summary>
        /// <param name="filter">The filter condition.</param>
        /// <returns>True if any entity satisfies the filter, else false.</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Counts the number of entities of type T that satisfy the given filter.
        /// </summary>
        /// <param name="filter">The filter condition.</param>
        /// <returns>The count of entities that satisfy the filter.</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Counts the total number of entities of type T in the database.
        /// </summary>
        /// <returns>The total count of entities.</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Returns the entity of type T with the given id.
        /// </summary>
        /// <param name="id">The id of the entity to retrieve.</param>
        /// <returns>The entity with the given id.</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Returns a paged list of entities of type T from the database.
        /// </summary>
        /// <param name="pageIndex">The index of the page to retrieve.</param>
        /// <param name="pageNumber">The number of entities to include in each page.</param>
        /// <returns>A paged list of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageNumber);

        /// <summary>
        /// get all entities but paging entities
        /// </summary>
        /// <param name="pageNumber">index of list object T</param>
        /// <param name="pageSize">size of list objects T</param>
        /// <returns>list object T</returns>
        Task<Pagination<T>> ToPagination(int pageNumber = 0, int pageSize = 10);
        /// <summary>
        /// Updates the given entity of type T in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Updates a collection of entities of type T in the database.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Deletes the given entity of type T from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes a collection of entities of type T from the database.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Deletes the entity of type T with the given id from the database.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        Task Delete(object id);

        /// <summary>
        /// Gets the state of the specified entity and returns a string representation of that state.
        /// </summary>
        /// <param name="entity">The entity to check the state of.</param>
        /// <returns>A string representation of the state of the entity.</returns>
        string GetEntityState(T entity);

    }
}
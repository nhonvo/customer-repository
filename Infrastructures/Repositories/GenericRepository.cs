using Application.Commons;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructures.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        // create
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        // Read
        public async Task<Pagination<T>> ToPagination(int pageIndex = 0, int pageSize = 10)
        {
            var itemCount = await _dbSet.CountAsync();
            var items = await _dbSet.Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();

            var result = new Pagination<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter) => await _dbSet.AnyAsync(filter);
        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
                return await _dbSet.CountAsync();
            return await _dbSet.CountAsync(filter);
        }
        public async Task<int> CountAsync() => await _dbSet.CountAsync();
        public async Task<T> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageNumber) => await _dbSet.Skip(pageIndex).Take(pageNumber).ToListAsync();

        // Update
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        // delete
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public async Task Delete(object id)
        {
            T entity = await GetByIdAsync(id);
            Delete(entity);
        }
        // checking
        public string GetEntityState(T entity)
        {
            EntityState state = _dbSet.Entry(entity).State;
            switch (state)
            {
                case EntityState.Added:
                    return "Object is being inserted";
                case EntityState.Modified:
                    return "Object is being updated";
                case EntityState.Deleted:
                    return "Object is being deleted";
                case EntityState.Detached:
                    _dbSet.Attach(entity);
                    return "Object is being attached";
                case EntityState.Unchanged:
                    _dbSet.Entry(entity).State = EntityState.Detached;
                    return "Object is being detached";
                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }
        }



    }
}

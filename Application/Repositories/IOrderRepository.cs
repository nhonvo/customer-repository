using Domain.Entities;

namespace Application.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        /// <summary>
        /// Load related data of an Order entity using eager loading for its related entities.
        /// </summary>
        /// <param name="id">The ID of the Order entity to be loaded</param>
        /// <returns>The Order entity with its related entities eagerly loaded</returns>
        Order LoadRelatedData(int id);
    }
}
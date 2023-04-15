using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
        public Order LoadRelatedData(int id) => _dbSet.Where(x => x.Id == id)
                .Include(x => x.OrderDetails)
                .FirstOrDefault()!;
    }
}
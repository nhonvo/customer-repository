using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
        public Product LoadRelatedData(int id) => _dbSet.Where(x => x.Id == id)
                                                        .Include(x => x.OrderDetails)
                                                        .FirstOrDefault()!;

        public List<OrderDetails> SplitQuery(int id)
        {
            var query = _dbSet.Include(x => x.OrderDetails);
            var product = query.AsSplitQuery().ToList();
            var orderDetails = query.SelectMany(x => x.OrderDetails).AsSplitQuery().ToList();
            return orderDetails;
        }
    }
}
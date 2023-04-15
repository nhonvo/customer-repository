using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class ShippingCompanyRepository : GenericRepository<ShippingCompany>, IShippingCompanyRepository
    {
        public ShippingCompanyRepository(ApplicationDbContext context) : base(context)
        {
        }
        public ShippingCompany LoadRelatedData(int id) => _dbSet.Where(x => x.Id == id)
              .Include(x => x.Orders)
              .FirstOrDefault()!;
    }
}
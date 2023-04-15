using System.Collections.ObjectModel;
using Application;
using Application.Repositories;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }
        public List<Customer> LoadCustomersWithOrders() => _dbSet.Include(c => c.Orders).ToList();

        public List<Customer> LoadCustomersWithOrdersAndAddress() => _dbSet.Include(c => c.Orders).ToList();
        public List<Customer> LoadCustomersWithOrdersAndOrderDetailsAndProduct() => _dbSet.Include(c => c.Orders)
                                                                                         .ThenInclude(o => o.OrderDetails)
                                                                                         .ThenInclude(od => od.Product)
                                                                                         .ToList();
        public List<CustomerOrderDTO> LoadCustomersAndOrders() =>
            _dbSet.Join(_context.Orders,
                        customer => customer.Id,
                        order => order.CustomerId,
                        (customerMatch, orderMatch) => new CustomerOrderDTO
                        {
                            Customer = customerMatch,
                            Order = orderMatch
                        }).ToList();

        public Customer LoadCustomerWithAddressAndOrders(int id)
        {
            var customer = _dbSet.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                _context.Entry(customer).Collection(x => x.Orders).Load();
            }
            return customer;
        }
        public Customer LoadCustomerWithAddressAndOrdersAndOrderDetailsAndProduct(int id)
        {
            var customer = _dbSet.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                _context.Entry(customer).Collection(x => x.Orders)
                                        .Query()
                                        .Include(o => o.OrderDetails)
                                        .ThenInclude(od => od.Product)
                                        .ToList();
            }
            return customer;
        }
        public Customer LoadRelatedData(int id) => _dbSet.Where(x => x.Id == id)
                .Include(x => x.Orders)
                .FirstOrDefault()!;
        public ICollection<Order> LazyLoading()
        {
            var customer = _dbSet.FirstOrDefault();
            var order = customer.Orders;
            return order;
        }
        public List<Order> SplitQuery(int id)
        {
            var query = _dbSet.Include(x => x.Orders);
            var customer = query.AsSplitQuery().ToList();
            var order = query.SelectMany(x => x.Orders).AsSplitQuery().ToList();
            return order;
        }
        public List<Customer> CustomerGlobalFilter() => _dbSet.ToList();
        public List<Customer> CustomerNoGlobalFilter() => _dbSet.IgnoreQueryFilters().ToList();
    }
}
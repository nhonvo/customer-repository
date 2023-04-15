using Application;
using Application.Repositories;
using Applications.InterfaceRepositories;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        private bool _disposed;
        private readonly ICustomerRepository _repoCustomers;
        private readonly IOrderRepository _repoOrders;
        private readonly IShippingCompanyRepository _repoShippingCompanies;
        private readonly IProductRepository _repoProducts;
        private readonly IOrderDetailRepository _repoOrderDetails;
        private readonly IUserRepository _repoUser;
        public UnitOfWork(ApplicationDbContext context,
                            ICustomerRepository CustomerRepository,
                            IOrderRepository OrderRepository,
                            IShippingCompanyRepository ShippingCompanyRepository,
                            IProductRepository ProductRepository,
                            IUserRepository UserRepository,
                            IOrderDetailRepository OrderDetailRepository)
        {
            _context = context;
            _repoCustomers = CustomerRepository;
            _repoOrders = OrderRepository;
            _repoShippingCompanies = ShippingCompanyRepository;
            _repoProducts = ProductRepository;
            _repoUser = UserRepository;
            _repoOrderDetails = OrderDetailRepository;
        }
        public ICustomerRepository repoCustomers => _repoCustomers;
        public IOrderRepository repoOrders => _repoOrders;
        public IShippingCompanyRepository repoShippingCompanies => _repoShippingCompanies;
        public IProductRepository repoProducts => _repoProducts;
        public IOrderDetailRepository repoOrderDetails => _repoOrderDetails;
        public IUserRepository repoUser => _repoUser;

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
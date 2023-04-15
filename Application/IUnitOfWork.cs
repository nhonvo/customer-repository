using Application.Repositories;
using Applications.InterfaceRepositories;

namespace Application
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save any changes made in the context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves any changes made in the context to the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Begins a new transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        void Commit();

        /// <summary>
        /// Asynchronously commits the transaction.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Rolls back the transaction.
        /// </summary>
        void Rollback();
        public ICustomerRepository repoCustomers { get; }
        public IOrderRepository repoOrders { get; }
        public IShippingCompanyRepository repoShippingCompanies { get; }
        public IProductRepository repoProducts { get; }
        public IOrderDetailRepository repoOrderDetails { get; }
        public IUserRepository repoUser { get; }
    }
}
using Application.ViewModels;
using Domain.Entities;

namespace Application.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        /// <summary>
        /// Loads customers with their orders.
        /// </summary>
        /// <returns>A list of customers with their orders loaded.</returns>
        List<Customer> LoadCustomersWithOrders();

        /// <summary>
        /// Loads customers with their orders and address.
        /// </summary>
        /// <returns>A list of customers with their orders and address loaded.</returns>
        List<Customer> LoadCustomersWithOrdersAndAddress();

        /// <summary>
        /// Loads customers with their orders, order details, and product.
        /// </summary>
        /// <returns>A list of customers with their orders, order details, and product loaded.</returns>
        List<Customer> LoadCustomersWithOrdersAndOrderDetailsAndProduct();

        /// <summary>
        /// Loads customers and their associated orders using inner join.
        /// </summary>
        /// <returns>A list of customers and their associated orders loaded using inner join.</returns>
        List<CustomerOrderDTO> LoadCustomersAndOrders();

        /// <summary>
        /// Loads customer with their address and orders.
        /// </summary>
        /// <param name="id">The ID of the customer to load.</param>
        /// <returns>The customer with their address and orders loaded.</returns>
        Customer LoadCustomerWithAddressAndOrders(int id);

        /// <summary>
        /// Loads customer with their address, orders, order details, and product.
        /// </summary>
        /// <param name="id">The ID of the customer to load.</param>
        /// <returns>The customer with their address, orders, order details, and product loaded.</returns>
        Customer LoadCustomerWithAddressAndOrdersAndOrderDetailsAndProduct(int id);

        /// <summary>
        /// Loads a single related entity using lazy loading.
        /// </summary>
        /// <param name="id">The ID of the customer to load.</param>
        /// <returns>The customer with their related data loaded using lazy loading.</returns>
        Customer LoadRelatedData(int id);

        /// <summary>
        /// Loads related data using lazy loading.
        /// </summary>
        /// <returns>A list of orders loaded using lazy loading.</returns>
        ICollection<Order> LazyLoading();

        /// <summary>
        /// Loads orders using split queries.
        /// </summary>
        /// <param name="id">The ID of the customer to load orders for.</param>
        /// <returns>A list of orders loaded using split queries.</returns>
        List<Order> SplitQuery(int id);

        /// <summary>
        /// Loads customers with a global filter applied.
        /// </summary>
        /// <returns>A list of customers with a global filter applied.</returns>
        List<Customer> CustomerGlobalFilter();

        /// <summary>
        /// Loads customers without a global filter applied.
        /// </summary>
        /// <returns>A list of customers without a global filter applied.</returns>
        List<Customer> CustomerNoGlobalFilter();
    }
}
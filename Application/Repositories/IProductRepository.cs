using Domain.Entities;

namespace Application.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        /// <summary>
        /// Loads related data of a Product entity using lazy loading.
        /// </summary>
        /// <param name="id">The ID of the Product entity to load related data for.</param>
        /// <returns>The Product entity with the related data loaded.</returns>
        Product LoadRelatedData(int id);

        /// <summary>
        /// Uses Split Queries to load the OrderDetails related to a specific product.
        /// </summary>
        /// <param name="id">The ID of the product to load OrderDetails for.</param>
        /// <returns>A list of OrderDetails related to the specified product.</returns>
        List<OrderDetails> SplitQuery(int id);

    }
}
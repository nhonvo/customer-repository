using Domain.Entities;

namespace Application.Repositories
{
    public interface IShippingCompanyRepository : IGenericRepository<ShippingCompany>
    {
        // <summary>
        // Loads the related data for a ShippingCompany entity based on the provided ID, using lazy loading.
        // </summary>
        // <param name="id">The ID of the ShippingCompany entity to load the related data for.</param>
        // <returns>The ShippingCompany entity with its related data loaded.</returns>
        ShippingCompany LoadRelatedData(int id);
    }
}
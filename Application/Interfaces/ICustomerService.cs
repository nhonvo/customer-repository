using Application.Commons;
using Application.ViewModels;
using Application.ViewModels.AppResult;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Adds a new customer asynchronously.
        /// </summary>
        /// <param name="customerDTO">The DTO object representing the customer to be added.</param>
        /// <returns>An API result indicating whether the operation was successful or not.</returns>
        Task<ApiResult<bool>> AddAsync(CustomerDTO customerDTO);
        /// <summary>
        /// get a list of customers but paging
        /// </summary>
        /// <param name="pageIndex">index of list customers</param>
        /// <param name="pageSize">size of list customers</param>
        /// <returns></returns>
        Task<ApiResult<Pagination<Customer>>> GetAsync(int pageIndex = 0, int pageSize = 10);
        /// <summary>
        /// Updates an existing customer asynchronously.
        /// </summary>
        /// <param name="request">The object containing the customer update request.</param>
        /// <returns>An API result indicating whether the operation was successful or not.</returns>
        Task<ApiResult<bool>> UpdateAsync(CustomerUpdateRequest request);

        /// <summary>
        /// Deletes a customer asynchronously.
        /// </summary>
        /// <param name="id">The ID of the customer to be deleted.</param>
        /// <returns>An API result indicating whether the operation was successful or not.</returns>
        Task<ApiResult<bool>> DeleteAsync(int id);

        /// <summary>
        /// Partially updates an existing customer asynchronously.
        /// </summary>
        /// <param name="id">The ID of the customer to be updated.</param>
        /// <param name="customerPatch">The JSON patch document containing the partial update information.</param>
        /// <returns>An API result indicating whether the operation was successful or not.</returns>
        Task<ApiResult<bool>> PatchAsync(int id, JsonPatchDocument<CustomerDTO> customerPatch);
        /// <summary>
        /// get a customer by id
        /// </summary>
        /// <param name="id">id of customer</param>
        /// <returns>customer object</returns>
        Task<ApiResult<Customer>> GetByIdAsync(int id);

    }
}
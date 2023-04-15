using Application.Commons;
using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.AppResult;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memory;
        }
        public async Task<ApiResult<bool>> AddAsync(CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.repoCustomers.AddAsync(customer);
                await _unitOfWork.CommitAsync();
                return new ApiSuccessResult<bool>();

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<bool>("Failed to add customer!" + ex.ToString());
            }
        }
        public async Task<ApiResult<Pagination<Customer>>> GetAsync(int pageIndex = 0, int pageSize = 10)
        {
            if (_memoryCache.TryGetValue($"listCustomer {pageIndex} - {pageSize}", out Pagination<Customer> customers))
                return new ApiSuccessResult<Pagination<Customer>>(customers);

            customers = await _unitOfWork.repoCustomers.ToPagination(pageIndex, pageSize);
            _memoryCache.Set($"listCustomer {pageIndex} - {pageSize}", customers, TimeSpan.FromMinutes(10));
            if (customers != null)
                return new ApiSuccessResult<Pagination<Customer>>(customers);
            return new ApiErrorResult<Pagination<Customer>>("Failed to get customers!");
        }
        public async Task<ApiResult<bool>> UpdateAsync(CustomerUpdateRequest request)
        {
            var customer = await _unitOfWork.repoCustomers.GetByIdAsync(request.Id);
            if (customer == null)
                return new ApiErrorResult<bool>("Customer not found!");
            try
            {
                _unitOfWork.BeginTransaction();
                _mapper.Map<Customer>(request);
                _unitOfWork.repoCustomers.Update(customer);
                await _unitOfWork.CommitAsync();
                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<bool>("Failed to update customer!" + ex.ToString());
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            var customer = await _unitOfWork.repoCustomers.GetByIdAsync(id);
            if (customer == null)
            {
                return new ApiErrorResult<bool>("Customer not found!");
            }
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.repoCustomers.Delete(customer);
                await _unitOfWork.CommitAsync();
                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<bool>("Failed to delete customer!" + ex.ToString());
            }
        }

        public async Task<ApiResult<bool>> PatchAsync(int id, JsonPatchDocument<CustomerDTO> customerPatch)
        {
            var customer = await _unitOfWork.repoCustomers.GetByIdAsync(id);
            if (customer == null)
            {
                return new ApiErrorResult<bool>("Customer not found!");
            }
            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            customerPatch.ApplyTo(customerDTO);
            _mapper.Map(customerDTO, customer);
            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.repoCustomers.Update(customer);
                await _unitOfWork.CommitAsync();
                return new ApiSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<bool>("Failed to update customer!" + ex.ToString());
            }
        }
        public async Task<ApiResult<Customer>> GetByIdAsync(int id)
        {
            if (_memoryCache.TryGetValue("Customer", out Customer customer))
                return new ApiSuccessResult<Customer>(customer);

            customer = await _unitOfWork.repoCustomers.GetByIdAsync(id);
            _memoryCache.Set("Customer", customer);
            if (customer != null)
                return new ApiSuccessResult<Customer>(customer);
            return new ApiErrorResult<Customer>("Customer not found!");
        }

    }
}
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
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache memory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memory;
        }
        // TODO finish add order method user random 
        public async Task<ApiResult<bool>> AddAsync(OrderDTO request)
        {
            if (!await _unitOfWork.repoOrders.AnyAsync(x => x.ShippingCompanyId == request.ShippingCompanyId))
                return new ApiErrorResult<bool>("Shipping Company do not exist in database!");
            if (!await _unitOfWork.repoOrders.AnyAsync(x => x.CustomerId == request.CustomerId))
                return new ApiErrorResult<bool>("Customer do not exist in database!");
            var product = await _unitOfWork.repoProducts.GetAllAsync(0, 10);

            var order = _mapper.Map<Order>(request);
            order.OrderDetails = new List<OrderDetails>()
            {
                new OrderDetails{
                    ProductId = 1,
                }
            };

            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.repoOrders.AddAsync(order);
                await _unitOfWork.CommitAsync();
                return new ApiSuccessResult<bool>();

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<bool>("Failed to add customer!" + ex.ToString());
            }
        }
    }
}
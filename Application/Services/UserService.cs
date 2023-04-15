using Application;
using Application.ViewModels.AppResult;
using Applications.InterfaceServices;
using Applications.Utils;
using Applications.ViewModels.UserViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Applications.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public UserService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }
        public async Task<IEnumerable<User>> GetUsersAsync(int pageIndex, int pageNumber)
            => await _unitOfWork.repoUser.GetAllAsync(pageIndex, pageNumber);
        public async Task<ApiResult<LoginResponseViewModel>> LoginAsync(LoginRequestViewModel request)
        {
            var user = await _unitOfWork.repoUser.FindUserByUserName(request.UserName);
            // verify passwordHash
            if (!BCrypt.Net.BCrypt.Verify(request.PassWord, user.PasswordHash))
                return new ApiErrorResult<LoginResponseViewModel>("Incorrect Password!!!");
            var newUser = _mapper.Map<LoginResponseViewModel>(user);
            if (_memoryCache.TryGetValue("token", out string token))
            {
                newUser.Token = token;
                return new ApiSuccessResult<LoginResponseViewModel>(newUser);
            }
            newUser.Token = user.GenerateJWT(_configuration);
            newUser.ExpireDay = DateTime.Now.AddDays(1);
            _memoryCache.Set("token", newUser, TimeSpan.FromDays(1));
            return new ApiSuccessResult<LoginResponseViewModel>(newUser);
        }
        public async Task<ApiResult<bool>> RegisterAsync(RegisterRequestViewModel user)
        {
            // check userName is exist or not
            var isExist = await _unitOfWork.repoUser.ExistedUser(user.UserName);

            if (isExist)
                return new ApiErrorResult<bool>("UserName already Exist!!!");

            // create new User
            var newUser = new User
            {
                UserName = user.UserName,
                PasswordHash = user.PassWord.Hash(),
                DateOfBirth = user.DateOfBirth,
                Role = user.Role
            };

            await _unitOfWork.repoUser.AddAsync(newUser);
            await _unitOfWork.CommitAsync();
            return new ApiSuccessResult<bool>();
        }
    }
}

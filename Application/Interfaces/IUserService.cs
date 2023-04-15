using Application.ViewModels.AppResult;
using Applications.ViewModels.UserViewModels;
using Domain.Entities;

namespace Applications.InterfaceServices
{
    public interface IUserService
    {
        Task<ApiResult<LoginResponseViewModel>> LoginAsync(LoginRequestViewModel request);
        Task<IEnumerable<User>> GetUsersAsync(int pageIndex, int pageNumber);
        Task<ApiResult<bool>> RegisterAsync(RegisterRequestViewModel user);

    }
}

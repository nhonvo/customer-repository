using Application.Repositories;
using Domain.Entities;

namespace Applications.InterfaceRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindUserByUserName(string userName);
        Task<bool> ExistedUser(string userName);
    }
}

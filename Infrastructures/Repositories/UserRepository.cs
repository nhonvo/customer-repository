using Applications.InterfaceRepositories;
using Applications.InterfaceServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistedUser(string userName) => await _context.Users.AnyAsync(x => x.UserName == userName);

        public async Task<User> FindUserByUserName(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new Exception("Incorect UserName!!!");
            }

            return user;
        }
    }
}

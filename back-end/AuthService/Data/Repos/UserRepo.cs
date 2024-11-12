using AuthService.Data.Context;
using AuthService.Domain.Contracts.Repos;
using AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly DbSet<User> _dbSet;
        public UserRepo(AuthContext context) => _dbSet = context.Users;

        public async Task<User?> Find(long id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User?> FindByEmail(string email) => await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
}

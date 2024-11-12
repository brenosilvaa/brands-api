using AuthService.Domain.Models;

namespace AuthService.Domain.Contracts.Repos
{
    public interface IUserRepo
    {
        Task<User?> Find(long id);
        Task<User?> FindByEmail(string email);
    }
}

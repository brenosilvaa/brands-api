using AuthService.Domain.Contracts.Repos;

namespace AuthService.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IUserRepo Users { get; }
    }
}

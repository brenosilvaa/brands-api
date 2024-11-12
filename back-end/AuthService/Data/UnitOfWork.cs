using AuthService.Data.Context;
using AuthService.Data.Repos;
using AuthService.Domain.Contracts;
using AuthService.Domain.Contracts.Repos;

namespace AuthService.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthContext _context;
        public UnitOfWork(AuthContext context) => _context = context;

        private UserRepo _userRepo;
        public IUserRepo Users { get => _userRepo ??= new(_context); }
    }
}

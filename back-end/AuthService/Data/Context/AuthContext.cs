using AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data.Context
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthContext).Assembly);
        }

        #region DbSets
        public DbSet<User> Users { get; set; }
        #endregion
    }
}

using Microsoft.EntityFrameworkCore;
using BrandService.Domain.Contracts.Context;
using BrandService.Domain.Models;

namespace BrandService.Data.Context
{
    public class BrandsContext : DbContext, IDataContext
    {
        public BrandsContext(DbContextOptions<BrandsContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrandsContext).Assembly);
        }

        #region DbSets
        public DbSet<Brand> Brands { get; set; }
        #endregion
    }
}

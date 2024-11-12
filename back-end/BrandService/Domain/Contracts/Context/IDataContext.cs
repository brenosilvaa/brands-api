using Microsoft.EntityFrameworkCore;

namespace BrandService.Domain.Contracts.Context
{
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}

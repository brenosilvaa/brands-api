using Microsoft.EntityFrameworkCore;
using BrandService.Domain.Contracts;
using BrandService.Domain.Contracts.Context;
using BrandService.Domain.Contracts.Repos;

namespace BrandService.Data.Repos
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class, IBaseDomain
    {
        protected readonly DbSet<T> dbSet;
        public BaseRepo(IDataContext context) => dbSet = context.Set<T>();

        public virtual async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

        public virtual async Task<T?> FindAsync(long id) => await dbSet.FindAsync(id);

        public virtual async Task<IList<T>> ListAsync() => await dbSet.ToListAsync();

        public virtual void Remove(T entity) => dbSet.Remove(entity);

        public virtual void Update(T entity) => dbSet.Update(entity);
    }
}

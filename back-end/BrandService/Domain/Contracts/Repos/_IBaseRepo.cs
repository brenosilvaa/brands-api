namespace BrandService.Domain.Contracts.Repos
{
    public interface IBaseRepo<T> where T : IBaseDomain
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<IList<T>> ListAsync();
        Task<T?> FindAsync(long id);
    }
}

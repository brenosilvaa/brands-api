using BrandService.Domain.Contracts.Repos;

namespace BrandService.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        #region Repos
        IBrandRepo Brands { get;}
        #endregion

        #region Methods
        Task Commit();
        #endregion
    }
}

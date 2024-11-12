using BrandService.Data.Context;
using BrandService.Data.Repos;
using BrandService.Domain.Contracts;
using BrandService.Domain.Contracts.Repos;

namespace BrandService.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BrandsContext _context;
        public UnitOfWork(BrandsContext context) => _context = context;

        #region Repos
        private BrandRepo _brandRepo;
        public IBrandRepo Brands => _brandRepo ??= new BrandRepo(_context);
        #endregion

        #region Methods
        public async Task Commit() => await _context.SaveChangesAsync();

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

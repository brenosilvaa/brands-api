using BrandService.Domain.Models;

namespace BrandService.Domain.Contracts.Repos
{
    public interface IBrandRepo : IBaseRepo<Brand>
    {
        Task<IList<Brand>> SearchAsync(string searchTerms);
    }
}

using Microsoft.EntityFrameworkCore;
using BrandService.Data.Context;
using BrandService.Domain.Contracts.Repos;
using BrandService.Domain.Models;

namespace BrandService.Data.Repos
{
    public class BrandRepo : BaseRepo<Brand>, IBrandRepo
    {
        public BrandRepo(BrandsContext context) : base(context) { }

        public override async Task<IList<Brand>> ListAsync()
            => await dbSet.OrderBy(x => x.Name).ToListAsync();

        public async Task<IList<Brand>> SearchAsync(string searchTerms)
        {
            return await dbSet.Where(x => x.Name.Trim().ToLower().Contains(searchTerms.Trim().ToLower()))
                              .OrderBy(x => x.Name)
                              .ToListAsync();
        }
    }
}

using BrandService.Domain.ViewModels.Brands;

namespace BrandService.Domain.ViewModels.Searchs
{
    public class GlobalSearchVm
    {
        public IList<BrandVm>? Brands { get; set; }

        public GlobalSearchVm(
            IList<BrandVm>? brands = null)
        {
            Brands = brands;
        }
    }
}


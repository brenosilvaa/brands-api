using System.ComponentModel.DataAnnotations;

namespace BrandService.Domain.ViewModels.Brands
{
    public class CreateBrandVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

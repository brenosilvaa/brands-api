using System.ComponentModel.DataAnnotations;

namespace BrandService.Domain.ViewModels.Brands
{

    public class BrandVm
    {
        [Required]
        [ValidateId(ErrorMessage = "O Id não pode ser zero.")]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public string Description { get; set; }

        public class ValidateIdAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return value is long id && id > 0;
            }
        }
    }
}

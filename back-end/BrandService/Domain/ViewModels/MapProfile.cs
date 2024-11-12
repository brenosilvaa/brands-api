using AutoMapper;

using BrandService.App.Commands.Brands;
using BrandService.Domain.Models;
using BrandService.Domain.ViewModels.Brands;

namespace BrandService.Domain.ViewModels
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Brand
            CreateMap<Brand, BrandVm>().ReverseMap();
            CreateMap<CreateBrandVm, CreateBrandCommand>();
            CreateMap<CreateBrandVm, UpdateBrandCommand>();
            #endregion
        }
    }
}

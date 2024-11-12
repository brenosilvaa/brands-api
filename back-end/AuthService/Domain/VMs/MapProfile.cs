using AuthService.App.Commands.Users;
using AuthService.Domain.Models;
using AuthService.Domain.VMs.Users;
using AutoMapper;

namespace AuthService.Domain.VMs
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserVm>().ReverseMap();
            CreateMap<LoginCommand, LoginVm > ().ReverseMap();
        }
    }
}

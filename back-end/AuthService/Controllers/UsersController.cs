using AuthService.App.Commands.Users;
using AuthService.Domain.VMs.Users;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<UserVm> Login([FromBody] LoginVm login)
        {
            var command = _mapper.Map<LoginCommand>(login);

            return await _mediator.Send(command);
        }
    }
}

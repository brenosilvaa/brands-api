using AuthService.Domain.Contracts;
using AuthService.Domain.Utils;
using AuthService.Domain.VMs.Users;
using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.App.Commands.Users
{
    public class LoginCommand : IRequest<UserVm>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, UserVm>
        {
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public LoginCommandHandler(IUnitOfWork uow, IMapper mapper)
            {
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<UserVm> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                    throw new Exception("Usuário ou senha inválidos.");

                var user = await _uow.Users.FindByEmail(request.Email);

                if (user == null)
                    throw new Exception("Usuário ou senha inválidos.");
                var enc = request.Password.PasswordEncrypt("a1b2c3d4");
                if (user.Password != request.Password.PasswordEncrypt("a1b2c3d4"))
                    throw new Exception("Usuário ou senha inválidos.");

                var userVm = _mapper.Map<UserVm>(user);

                var (token, expiration) = GenerateToken(user.Id);
                userVm.Token = token;
                userVm.TokenExpiration = expiration;

                return userVm;
            }

            private static (string, DateTime) GenerateToken(long subject)
            {
                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, subject.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var tokenExpiration = DateTime.UtcNow.AddHours(12);

                var token = new JwtSecurityToken(
                    issuer: "br.com",
                    audience: "br.com",
                    claims: claims,
                expires: tokenExpiration,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("V3h!cLe@2os3TesTV3h!cLe@2os3TesT")), SecurityAlgorithms.HmacSha256)
                );

                return (new JwtSecurityTokenHandler().WriteToken(token), tokenExpiration);
            }
        }
    }
}

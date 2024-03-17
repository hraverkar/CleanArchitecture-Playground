using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Login.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CleanArchitecture.Application.Login.Commands
{
    public class CreateLoginCommand(LoginDto loginDto) : IRequest<TokenDto>
    {
        public LoginDto LoginDto { get; set; } = loginDto;
    }
    public class CreateLoginCommandHandler(IUnitOfWork unitOfWork, IConfiguration config) : IRequestHandler<CreateLoginCommand, TokenDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _config = config;

        public async Task<TokenDto> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            var tokenDto = new TokenDto { Token = token, ExpireTime = Sectoken.ValidTo, UserName = request.LoginDto.UserName };
            return tokenDto;
        }
    }
}
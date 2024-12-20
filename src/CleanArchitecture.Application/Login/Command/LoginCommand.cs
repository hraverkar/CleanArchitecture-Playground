using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Login.Models;
using CleanArchitecture.Core.Weather.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchitecture.Application.Login.Command
{
    public sealed record LoginCommand(LoginDto LoginDto) : CommandBase<TokenDto>;
    public sealed class LoginCommandHandler : CommandHandler<LoginCommand, TokenDto>
    {
        private readonly IRepository<RegisterUser> _repository;
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(IRepository<RegisterUser> repository, IUnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork)
        {
            _repository = repository;
            _configuration = configuration;
        }


        protected async override Task<TokenDto> HandleAsync(LoginCommand request, CancellationToken cancellationToken)
        {
            var userRecord = _repository.GetAll(false).SingleOrDefault(a => a.Email == request.LoginDto.Email);

            if (userRecord == null || !BCrypt.Net.BCrypt.Verify(request.LoginDto.Password, userRecord?.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiryDate = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]));
            var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                      claims: new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime),
                            new Claim(JwtRegisteredClaimNames.Sub, userRecord.UserName),
                            new Claim(JwtRegisteredClaimNames.Email, userRecord.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        },
                        notBefore: DateTime.UtcNow,
                        expires: expiryDate,
                        signingCredentials: credentials);
            var tokenDto = new TokenDto { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), ExpireTime = expiryDate, Email = userRecord.Email, UserName = userRecord.UserName };
            return tokenDto;
        }
    }
}
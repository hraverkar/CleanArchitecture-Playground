using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Authors.Models;
using CleanArchitecture.Application.Login.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Locations.Entities;
using CleanArchitecture.Core.Weather.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CleanArchitecture.Application.Login.Commands
{
    public sealed record CreateLoginQuery(string Email, string Password) : Query<TokenDto>;
    public sealed class CreateLoginQueryHandler : QueryHandler<CreateLoginQuery, TokenDto>
    {
        private readonly IRepository<RegisterUser> _registerUserRepository;
        private readonly IConfiguration _configuration;

        public CreateLoginQueryHandler(IMapper mapper, IRepository<RegisterUser> repository, IConfiguration configuration) : base(mapper)
        {

            _registerUserRepository = repository;
            _configuration = configuration;
        }

        protected async override Task<TokenDto> HandleAsync(CreateLoginQuery request)
        {
            var userRecord = _registerUserRepository.GetAll(false).Where(a => a.Email == request.Email).ToList();

            if (userRecord.Count  == 0 || !BCrypt.Net.BCrypt.Verify(request.Password, userRecord?.FirstOrDefault()?.Password))
            {
                return new TokenDto { Token = null, ExpireTime = DateTime.Now, Email = null };
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiryDate = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]));
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                null,
                expires: expiryDate,
                signingCredentials: credentials);

            var tokenDto = new TokenDto { Token = new JwtSecurityTokenHandler().WriteToken(token), ExpireTime = expiryDate, Email = request.Email };
            return tokenDto;
        }
    }

}
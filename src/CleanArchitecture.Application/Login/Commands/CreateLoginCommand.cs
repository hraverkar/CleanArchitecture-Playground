using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;

using CleanArchitecture.Application.Abstractions.Services;
using CleanArchitecture.Application.Login.Models;
using CleanArchitecture.Core.CarCompanies.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Login.Commands
{
    public sealed record CreateLoginCommand(string UserName, string Password) : CreateCommand();
    public sealed class CreateLoginCommandHandler : CreateCommandHandler<CreateLoginCommand>
    {
        private IConfiguration _config;
        public CreateLoginCommandHandler(IUnitOfWork unitOfWork, IConfiguration config) : base(unitOfWork)
        {
            _config = config;
        }

        protected async override Task<string> HandleAsync(CreateLoginCommand request)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            return token;
        }
    }
}
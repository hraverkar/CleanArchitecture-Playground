using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Authors.Models;
using CleanArchitecture.Application.RegisterUsers.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Locations.Entities;
using CleanArchitecture.Core.Weather.Entities;

namespace CleanArchitecture.Application.Authors.Commands
{
    public sealed record RegisterUserCreateCommand(RegisterUserRequestDto registerUserRequestDto) : CreateCommand;
    public sealed class RegisterUserCreateCommandHandler : CreateCommandHandler<RegisterUserCreateCommand>
    {
        private readonly IRepository<RegisterUser> _repository;
        public RegisterUserCreateCommandHandler(IRepository<RegisterUser> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
        }

        protected override async Task<Guid> HandleAsync(RegisterUserCreateCommand request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.registerUserRequestDto.Password);
            var userInformation = _repository.GetAll(false).Where(r => r.Email == request.registerUserRequestDto.Email &&
            r.UserName == request.registerUserRequestDto.UserName).ToList();
            if (userInformation.Count == 0)
            {
                var userCreated = RegisterUser.Create(request.registerUserRequestDto.Email,
                passwordHash, request.registerUserRequestDto.UserName);
                _repository.Insert(userCreated);
                await UnitOfWork.CommitAsync();
                return userCreated.Id;
            }
            userInformation = Guard.Against.Found(userInformation, $"User Already found: {request.registerUserRequestDto.Email}");
            return Guid.Empty;
        }
    }
}
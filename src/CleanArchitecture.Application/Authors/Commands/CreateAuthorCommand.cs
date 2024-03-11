using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Core.Locations.Entities;

namespace CleanArchitecture.Application.Authors.Commands
{
    public sealed record CreateAuthorCommand(string Name, string Email, string Description) : CreateCommand;
    public sealed class CreateAuthorCommandHandler : CreateCommandHandler<CreateAuthorCommand>
    {
        private readonly IRepository<Author> _repository;
        public CreateAuthorCommandHandler(IRepository<Author> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
        }

        protected override async Task<string> HandleAsync(CreateAuthorCommand request)
        {
            var authorCreated = Author.Create(request.Name, request.Email, request.Description);
            _repository.Insert(authorCreated);
            await UnitOfWork.CommitAsync();
            return authorCreated.Id.ToString();
        }
    }
}
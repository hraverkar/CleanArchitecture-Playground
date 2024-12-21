using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Projects.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Projects.Entities;

namespace CleanArchitecture.Application.Projects.Commands
{
    public sealed record DeleteProjectByIdCommand(Guid Id) : CommandBase<string>;
    public sealed class DeleteProjectByIdCommandHandler : CommandHandler<DeleteProjectByIdCommand, string>
    {
        private readonly IRepository<Project> _repository;

        public DeleteProjectByIdCommandHandler(IRepository<Project> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
        }
        protected async override Task<string> HandleAsync(DeleteProjectByIdCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            var projectExists = await _repository.GetByIdAsync(request.Id);
            projectExists = Guard.Against.NotFound(projectExists);
            _repository.SoftDelete(projectExists);
            await UnitOfWork.CommitAsync();
            return await Task.FromResult("Record Deleted Successfully !!");
        }
    }
}

using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Projects.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Projects.Entities;

namespace CleanArchitecture.Application.Projects.Commands
{
    public sealed record UpdateProjectCommand(UpdateProjectRequestDto UpdateProjectRequestDto): CreateCommand;
    public sealed class UpdateProjectCommandHandler : CreateCommandHandler<UpdateProjectCommand>
    {
        private readonly IRepository<Project> _repository;
        public UpdateProjectCommandHandler(IUnitOfWork unitOfWork, IRepository<Project> repository) : base(unitOfWork)
        {
            _repository = repository;
        }

        protected async override Task<string> HandleAsync(UpdateProjectCommand request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var projectExists = await _repository.GetByIdAsync(request.UpdateProjectRequestDto.Id);
            projectExists = Guard.Against.NotFound(projectExists);
            projectExists.Update(request.UpdateProjectRequestDto.ProjectName,
                request.UpdateProjectRequestDto.ProjectDescription, request.UpdateProjectRequestDto.UpdatedAt,
                request.UpdateProjectRequestDto.UpdatedBy);
            _repository.Update(projectExists);
            await UnitOfWork.CommitAsync();
            return "Project Updated !!";
        }
    }
}

using Azure.Core;
using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Projects.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Projects.Entities;

namespace CleanArchitecture.Application.Projects.Commands
{
    public sealed record CreateProjectCommand(ProjectRequestDto ProjectRequestDto) : CreateCommand;
    public sealed class CreateProjectCommandHandler : CreateCommandHandler<CreateProjectCommand>
    {
        private readonly IRepository<Project> _repository;

        public CreateProjectCommandHandler(IRepository<Project> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
        }
        protected override async Task<Guid> HandleAsync(CreateProjectCommand request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));
            ArgumentNullException.ThrowIfNull(request.ProjectRequestDto, nameof(request.ProjectRequestDto));
            var projectExists = _repository.GetAll(false).FirstOrDefault(r => r.ProjectName == request.ProjectRequestDto.ProjectName);
            if (projectExists == null)
            {
                var projectCreate = Project.Create(
                    request.ProjectRequestDto.ProjectName,
                    request.ProjectRequestDto.ProjectDescription,
                    DateTime.Now,
                    request.ProjectRequestDto.CreatedBy
                    );
                _repository.Insert(projectCreate);
                await UnitOfWork.CommitAsync();
                return projectCreate.Id;
            }
            else
            {
                throw new InvalidOperationException($"Project '{request.ProjectRequestDto.ProjectName}' already exists. Please provide a unique name.");
            }
        }
    }
}


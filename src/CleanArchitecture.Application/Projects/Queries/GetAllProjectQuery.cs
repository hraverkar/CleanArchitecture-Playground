using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Projects.Models;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.BuildingBlocks.EventBus.Interfaces;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.IntegrationEvents;
using CleanArchitecture.Core.Projects.Entities;
using CleanArchitecture.Core.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Projects.Queries
{
    public sealed record class GetAllProjectQuery() : Query<ProjectResponseListDto>;
    public sealed class GetAllProjectQueryHandler : QueryHandler<GetAllProjectQuery, ProjectResponseListDto>
    {
        public readonly IRepository<Project> _repository;
        public IEventBus _eventBus;
        public GetAllProjectQueryHandler(IMapper mapper, IRepository<Project> repository, IEventBus eventBus) : base(mapper)
        {
            _repository = repository;
            _eventBus = eventBus;
        }
        protected async override Task<ProjectResponseListDto> HandleAsync(GetAllProjectQuery request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var allProjects = _repository.GetAll(false).Where(a => !a.IsDeleted).OrderByDescending(a => a.CreatedAt);

            if (allProjects != null)
            {
                Guard.Against.NotFound(allProjects);
                var projectResponseDtos = Mapper.Map<List<ProjectResponseDto>>(allProjects);
                int taskCount = projectResponseDtos.Count;
                var taskDetailsDto = new ProjectResponseListDto
                {
                    items = projectResponseDtos,
                    count = taskCount
                };
                await _eventBus.PublishAsync(new SampleAutomation1Event(Guid.NewGuid(), "GetAllProjectQuery"));
                return taskDetailsDto;
            }
            throw new ArgumentNullException(nameof(request));
        }
    }
}

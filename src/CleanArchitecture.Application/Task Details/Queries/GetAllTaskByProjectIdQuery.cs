using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Task_Details.Queries
{
    public sealed record class GetAllTaskByProjectIdQuery(Guid Id) : Query<TaskDetailsDto>;
    public sealed class GetAllTaskByProjectIdQueryHandler : QueryHandler<GetAllTaskByProjectIdQuery, TaskDetailsDto>
    {
        private readonly IRepository<TaskDetails> _taskDetailsRepository;
        public GetAllTaskByProjectIdQueryHandler(IMapper mapper, IRepository<TaskDetails> taskDetailsRepository) : base(mapper)
        {
            _taskDetailsRepository = taskDetailsRepository;
        }

        protected async override Task<TaskDetailsDto> HandleAsync(GetAllTaskByProjectIdQuery request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var taskDetails = _taskDetailsRepository
            .GetAll(false).Where(i => i.ProjectId == request.Id && !i.IsDeleted).Include(a => a.TaskStatus).Include(a => a.Project);
                Guard.Against.NotFound(taskDetails);
                if (taskDetails != null)
                {
                    var taskDetailsResponseDtos = Mapper.Map<List<TaskDetailsResponseDto>>(taskDetails);
                    int taskCount = taskDetailsResponseDtos.Count;
                    var taskDetailsDto = new TaskDetailsDto
                    {
                        items = taskDetailsResponseDtos,
                        count = taskCount
                    };
                    return taskDetailsDto;
                }
                else
                {
                    throw new ArgumentNullException(nameof(request));
                }
           
        }
    }
}
using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Core.Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Task_Details.Queries
{
    public sealed record GetAllTaskQuery() : Query<TaskDetailsDto>;
    public sealed class GetAllTaskQueryHandler : QueryHandler<GetAllTaskQuery, TaskDetailsDto>
    {
        private readonly IRepository<TaskDetails> _taskDetailsRepository;
        public GetAllTaskQueryHandler(IMapper mapper, IRepository<TaskDetails> taskDetailsRepository) : base(mapper)
        {
            _taskDetailsRepository = taskDetailsRepository;
        }
        protected async override Task<TaskDetailsDto> HandleAsync(GetAllTaskQuery request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var taskDetails = _taskDetailsRepository
            .GetAll(false)
            .Include(a => a.TaskStatus);
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

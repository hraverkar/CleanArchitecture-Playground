using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Core.Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Task_Details.Queries
{
    public sealed record GetTaskByFilterQuery(string TaskStatus, string TaskAssignTo, string TaskCreatedBy) : Query<TaskDetailsDto>;
    public sealed class GetTaskByFilterQueryHandler : QueryHandler<GetTaskByFilterQuery, TaskDetailsDto>
    {
        private readonly IRepository<TaskDetails> _taskDetailsRepository;
        public GetTaskByFilterQueryHandler(IMapper mapper, IRepository<TaskDetails> taskDetailsRepository) : base(mapper)
        {
            _taskDetailsRepository = taskDetailsRepository;
        }
        protected async override Task<TaskDetailsDto> HandleAsync(GetTaskByFilterQuery request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var query = _taskDetailsRepository.GetAll(false)
            .Where(a =>
            (request.TaskStatus == null || a.TaskStatus == request.TaskStatus) &&
            (request.TaskAssignTo == null || a.TaskAssignTo == request.TaskAssignTo) &&
            (request.TaskCreatedBy == null || a.TaskCreatedBy == request.TaskCreatedBy));
            var taskDetails = await query.ToListAsync() ?? throw new ArgumentNullException(nameof(request));
            var taskDetailsResponseDtos = Mapper.Map<List<TaskDetailsResponseDto>>(taskDetails);
            int taskCount = taskDetailsResponseDtos.Count;
            return new TaskDetailsDto
            {
                items = taskDetailsResponseDtos,
                count = taskCount
            };
        }
    }
}
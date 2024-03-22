using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Application.Task_Details.Queries;
using CleanArchitecture.Application.Task_Status.Model;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Task_Details.Task_Status_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;

namespace CleanArchitecture.Application.Task_Status.Queries
{
    public sealed record GetAllTaskStatusQuery() : Query<List<TaskStatusResponseDto>>;
    public sealed class GetAllTaskStatusQueryHandler : QueryHandler<GetAllTaskStatusQuery, List<TaskStatusResponseDto>>
    {
        private readonly IRepository<TaskStatus> _repository;
        public GetAllTaskStatusQueryHandler(IMapper mapper, IRepository<TaskStatus> repository) : base(mapper)
        {
            _repository = repository;
        }

        protected async override Task<List<TaskStatusResponseDto>> HandleAsync(GetAllTaskStatusQuery request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var taskStatus = _repository.GetAll(false);
            if (taskStatus != null)
            {
                Guard.Against.NotFound(taskStatus);
                return (Mapper.Map<List<TaskStatusResponseDto>>(taskStatus));
            }
            throw new ArgumentNullException(nameof(request));
        }
    }
}

using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Application.Task_Status.Model;
using CleanArchitecture.Core.Task.Entities;
using CleanArchitecture.Core.Task_Details.Task_Status_Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;

namespace CleanArchitecture.Application.Task_Status.Commands
{
    public sealed record CreateTaskStatusCommand(TaskStatusRequestDto TaskStatusRequestDto) : CreateCommand;
    public sealed class CreateTaskStatusCommandHandler : CreateCommandHandler<CreateTaskStatusCommand>
    {
        private readonly IRepository<TaskStatus> _taskStatusRepository;

        public CreateTaskStatusCommandHandler(IRepository<TaskStatus> taskStatusRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _taskStatusRepository = taskStatusRepository;
        }
        protected override async Task<string> HandleAsync(CreateTaskStatusCommand request)
        {
            var createTask = TaskStatus.Create(request.TaskStatusRequestDto.StatusName, request.TaskStatusRequestDto.CreatedBy,
                request.TaskStatusRequestDto.CreatedAt, request.TaskStatusRequestDto.IsDeleted);
            _taskStatusRepository.Insert(createTask);
            await UnitOfWork.CommitAsync();
            return createTask.Id.ToString();
        }
    }
}

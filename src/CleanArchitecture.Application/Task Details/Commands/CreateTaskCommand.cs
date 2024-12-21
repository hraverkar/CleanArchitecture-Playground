﻿using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Core.Task.Entities;

namespace CleanArchitecture.Application.Task_Details.Commands
{
    public sealed record CreateTaskCommand(TaskDetailsRequestDto TaskDetailsRequestDto) : CreateCommand;
    public sealed class CreateTaskCommandHandler : CreateCommandHandler<CreateTaskCommand>
    {
        private readonly IRepository<TaskDetails> _taskDetailsRepository;

        public CreateTaskCommandHandler(IRepository<TaskDetails> taskDetailsRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _taskDetailsRepository = taskDetailsRepository;
        }
        protected override async Task<Guid> HandleAsync(CreateTaskCommand request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var createTask = TaskDetails.Create(request.TaskDetailsRequestDto.TaskTitle,
                request.TaskDetailsRequestDto.TaskDetail, request.TaskDetailsRequestDto.TaskAssignTo,
                request.TaskDetailsRequestDto.TaskStatusId, DateTime.Now,
                request.TaskDetailsRequestDto.TaskCreatedBy, false, request.TaskDetailsRequestDto.ProjectId);
            _taskDetailsRepository.Insert(createTask);
            await UnitOfWork.CommitAsync();
            return await Task.FromResult(createTask.Id);
        }
    }
}
﻿using AutoMapper;
using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Core.Abstractions.Guards;
using MediatR;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;

namespace CleanArchitecture.Application.Task_Status.Queries
{
    public sealed record DeleteTaskDetailsCommand(Guid Id) : CommandBase<string>;
    public sealed class DeleteTaskStatusCommandHandler : CommandHandler<DeleteTaskDetailsCommand, string>
    {
        private readonly IRepository<TaskStatus> _repository;
        public DeleteTaskStatusCommandHandler(IRepository<TaskStatus> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
        }

        protected async override Task<string> HandleAsync(DeleteTaskDetailsCommand request, CancellationToken cancellationToken)
        {
            var taskStatus = await _repository.GetByIdAsync(request.Id);
            taskStatus = Guard.Against.NotFound(taskStatus);
            _repository.SoftDelete(taskStatus);
            await UnitOfWork.CommitAsync();
            return "Record Deleted Successfully !!";
        }
    }
}

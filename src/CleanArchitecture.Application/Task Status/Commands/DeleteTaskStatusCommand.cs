using AutoMapper;
using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Core.Abstractions.Guards;
using MediatR;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;

namespace CleanArchitecture.Application.Task_Status.Queries
{
    public sealed record DeleteTaskStatusCommand(Guid Id) : CreateCommand;
    public sealed class DeleteTaskStatusCommandHandler : CreateCommandHandler<DeleteTaskStatusCommand>
    {
        private readonly IRepository<TaskStatus> _repository;
        public DeleteTaskStatusCommandHandler(IRepository<TaskStatus> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
        }

        protected async override Task<string> HandleAsync(DeleteTaskStatusCommand request)
        {
            var taskStatus = _repository.GetByIdAsync(request.Id);
            _ = Guard.Against.NotFound(taskStatus);
            _repository.Delete(await taskStatus);
            await UnitOfWork.CommitAsync();
            return "Record Deleted Successfully !!";

        }
    }
}

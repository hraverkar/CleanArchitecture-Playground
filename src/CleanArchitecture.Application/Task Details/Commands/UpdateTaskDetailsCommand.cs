using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Task.Entities;

namespace CleanArchitecture.Application.Task_Details.Commands
{
    public sealed record UpdateTaskDetailsCommand(UpdateTaskDetailsRequestDto UpdateTaskDetailsRequestDto) : CreateCommand;
    public sealed class UpdateTaskDetailsCommandHandler(IRepository<TaskDetails> repository, IUnitOfWork unitOfWork) : CreateCommandHandler<UpdateTaskDetailsCommand>(unitOfWork)
    {
        private readonly IRepository<TaskDetails> _repository = repository;

        protected async override Task<string> HandleAsync(UpdateTaskDetailsCommand request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var taskDetailsExists = await _repository.GetByIdAsync(request.UpdateTaskDetailsRequestDto.Id);
            taskDetailsExists = Guard.Against.NotFound(taskDetailsExists);
            taskDetailsExists.Update(request.UpdateTaskDetailsRequestDto.TaskTitle,
                request.UpdateTaskDetailsRequestDto.TaskDetail, request.UpdateTaskDetailsRequestDto.TaskAssignTo,
                request.UpdateTaskDetailsRequestDto.TaskStatusId, request.UpdateTaskDetailsRequestDto.TaskCreatedBy);
            _repository.Update(taskDetailsExists);
            await UnitOfWork.CommitAsync();
            return "Task Updated !!";
        }
    }
}

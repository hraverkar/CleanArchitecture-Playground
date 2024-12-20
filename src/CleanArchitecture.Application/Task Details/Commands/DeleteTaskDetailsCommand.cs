using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Core.Abstractions.Guards;
using CleanArchitecture.Core.Task.Entities;

namespace CleanArchitecture.Application.Task_Details.Commands
{
    public sealed record DeleteTaskDetailsCommand(Guid Id) : CommandBase<string>;
    public sealed class DeleteTaskDetailsCommandHandler : CommandHandler<DeleteTaskDetailsCommand, string>
    {
        private readonly IRepository<TaskDetails> _repository;
        public DeleteTaskDetailsCommandHandler(IRepository<TaskDetails> repository, IUnitOfWork unitOfWork) : base(unitOfWork)
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

using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Core.Abstractions.Guards;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;

namespace CleanArchitecture.Application.Task_Status.Queries
{
    public sealed record class DeleteTaskStatusQuery(Guid Id) : Query<string>;
    public sealed class DeleteTaskStatusQueryHandler : QueryHandler<DeleteTaskStatusQuery, string>
    {
        private readonly IRepository<TaskStatus> _repository;
        public DeleteTaskStatusQueryHandler(IMapper mapper, IRepository<TaskStatus> repository) : base(mapper)
        {
            _repository = repository;
        }

        protected async override Task<string> HandleAsync(DeleteTaskStatusQuery request)
        {
            var taskStatus = _repository.GetByIdAsync(request.Id);
            _ = Guard.Against.NotFound(taskStatus);
             _repository.Delete(await taskStatus);
            return "Task Status Deleted successfully !!";
        }
    }
}

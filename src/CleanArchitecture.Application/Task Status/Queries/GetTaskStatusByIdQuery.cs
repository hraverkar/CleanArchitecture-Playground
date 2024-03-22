using AutoMapper;
using CleanArchitecture.Application.Abstractions.Queries;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Task_Status.Model;
using CleanArchitecture.Core.Abstractions.Guards;
using TaskStatus = CleanArchitecture.Core.Task_Details.Task_Status_Entities.TaskStatus;

namespace CleanArchitecture.Application.Task_Status.Queries
{
    public sealed record class GetTaskStatusByIdQuery(Guid Id) : Query<TaskStatusResponseDto>;
    public sealed class GetTaskStatusByIdQueryHandler : QueryHandler<GetTaskStatusByIdQuery, TaskStatusResponseDto>
    {
        private readonly IRepository<TaskStatus> _repository;

        public GetTaskStatusByIdQueryHandler(IMapper mapper, IRepository<TaskStatus> repository) : base(mapper)
        {
            _repository = repository;
        }

        protected async override Task<TaskStatusResponseDto> HandleAsync(GetTaskStatusByIdQuery request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var taskStatus = _repository.GetAll(false).FirstOrDefault(t => t.Id == request.Id);
            if (taskStatus != null)
            {
                Guard.Against.NotFound(taskStatus);
                return (Mapper.Map<TaskStatusResponseDto>(taskStatus));
            }
            throw new ArgumentNullException(nameof(request));
        }

    }
}

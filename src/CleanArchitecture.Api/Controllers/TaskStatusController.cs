using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Task_Status.Commands;
using CleanArchitecture.Application.Task_Status.Model;
using CleanArchitecture.Application.Task_Status.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class TaskStatusController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] TaskStatusRequestDto taskStatusRequestDto)
        {
            var id = await _mediator.Send(new CreateTaskStatusCommand(taskStatusRequestDto));
            return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelope(id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskStatusResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid Id)
        {
            var taskDetail = await _mediator.Send(new GetTaskStatusByIdQuery(Id));
            return Ok(taskDetail);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<TaskStatusResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var taskDetail = await _mediator.Send(new GetAllTaskStatusQuery());
            return Ok(taskDetail);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var message = await _mediator.Send(new DeleteTaskDetailsCommand(Id));
            return Ok(new ResponseMessage { Message = message });
        }
    }
}

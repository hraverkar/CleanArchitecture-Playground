using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Task_Details.Commands;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Application.Task_Details.Queries;
using CleanArchitecture.Application.Task_Status.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class TaskDetailsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] TaskDetailsRequestDto taskDetailsRequestDto)
        {
            var id = await _mediator.Send(new CreateTaskCommand(taskDetailsRequestDto));
            return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelopeGuid(id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskDetailsResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid Id)
        {
            var taskDetail = await _mediator.Send(new GetTaskByIdQuery(Id));
            return Ok(taskDetail);
        }

        [HttpGet("project/{id}")]
        [ProducesResponseType(typeof(TaskDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTaskDetailsByProjectId(Guid Id)
        {
            var taskDetail = await _mediator.Send(new GetAllTaskByProjectIdQuery(Id));
            return Ok(taskDetail);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(TaskDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var taskDetails = await _mediator.Send(new GetAllTaskQuery());
            return Ok(taskDetails);
        }

        [HttpGet("GetTask/{TaskStatus}/{TaskAssignTo}/{TaskCreatedBy}")]
        [ProducesResponseType(typeof(List<TaskDetailsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilterTask(Guid TaskStatus, string TaskAssignTo, string TaskCreatedBy)
        {
            var taskDetails = await _mediator.Send(new GetTaskByFilterQuery(TaskStatus, TaskAssignTo, TaskCreatedBy));
            return Ok(taskDetails);
        }

        [HttpPatch()]
        [ProducesResponseType(typeof(TaskDetailsResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTaskDetailsById(UpdateTaskDetailsRequestDto updateTaskDetailsRequestDto)
        {
            var taskDetail = await _mediator.Send(new UpdateTaskDetailsCommand(updateTaskDetailsRequestDto));
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

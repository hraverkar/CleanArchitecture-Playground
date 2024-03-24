using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Application.Task_Details.Queries;
using CleanArchitecture.Application.Task_Status.Commands;
using CleanArchitecture.Application.Task_Status.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class ProjectController(IMediator mediator) : ControllerBase
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
        [ProducesResponseType(typeof(TaskDetailsResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid Id)
        {
            var taskDetail = await _mediator.Send(new GetTaskByIdQuery(Id));
            return Ok(taskDetail);
        }
    }
}

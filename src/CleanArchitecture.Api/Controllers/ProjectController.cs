using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Projects.Commands;
using CleanArchitecture.Application.Projects.Models;
using CleanArchitecture.Application.Projects.Queries;
using CleanArchitecture.Application.Task_Details.Models;
using CleanArchitecture.Application.Task_Details.Queries;
using CleanArchitecture.Application.Task_Status.Commands;
using CleanArchitecture.Application.Task_Status.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> Post([FromBody] ProjectRequestDto projectRequestDto)
        {
            var id = await _mediator.Send(new CreateProjectCommand(projectRequestDto));
            return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelope(id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid Id)
        {
            var projectDetails = await _mediator.Send(new GetProjectByIdQuery(Id));
            return Ok(projectDetails);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(ProjectResponseListDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var projectDetails = await _mediator.Send(new GetAllProjectQuery());
            return Ok(projectDetails);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProjectResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProjectById(Guid Id)
        {
            var projectDetails = await _mediator.Send(new DeleteProjectByIdCommand(Id));
            return Ok(projectDetails);
        }

        [HttpPatch()]
        [ProducesResponseType(typeof(ProjectResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectById(UpdateProjectRequestDto updateProjectRequestDto)
        {
            var updatedProj = await _mediator.Send(new UpdateProjectCommand(updateProjectRequestDto));
            return Ok(updatedProj);
        }
    }
}

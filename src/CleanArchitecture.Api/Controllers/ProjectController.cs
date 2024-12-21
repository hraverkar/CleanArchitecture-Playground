using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Projects.Commands;
using CleanArchitecture.Application.Projects.Models;
using CleanArchitecture.Application.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class ProjectController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] ProjectRequestDto projectRequestDto)
        {
            var id = await _mediator.Send(new CreateProjectCommand(projectRequestDto));
            return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelopeGuid(id));
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

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProjectResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProjectById(Guid Id)
        {
            var projectDetails = await _mediator.Send(new DeleteProjectByIdCommand(Id));
            return Ok(projectDetails);
        }

        [Authorize]
        [HttpPatch()]
        [ProducesResponseType(typeof(ProjectResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProjectById(UpdateProjectRequestDto updateProjectRequestDto)
        {
            var updatedProj = await _mediator.Send(new UpdateProjectCommand(updateProjectRequestDto));
            return Ok(updatedProj);
        }
    }
}

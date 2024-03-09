using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Authors.Commands;
using CleanArchitecture.Application.Authors.Models;
using CleanArchitecture.Application.Authors.Queries;
using CleanArchitecture.Application.Weather.Commands;
using CleanArchitecture.Application.Weather.Models;
using CleanArchitecture.Application.Weather.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Guid Id)
        {
            var author = await _mediator.Send(new GetAuthorQuery(Id));
            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] AuthorCreateDto author)
        {
            var id = await _mediator.Send(new CreateAuthorCommand(author.Name, author.Email, author.Description));
            return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelope(id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteWeatherForecastCommand(id));
            return NoContent();
        }
    }
}
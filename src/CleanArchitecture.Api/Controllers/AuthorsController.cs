using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Authors.Commands;
using CleanArchitecture.Application.Authors.Models;
using CleanArchitecture.Application.Authors.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class AuthorsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid Id)
        {
            var author = await _mediator.Send(new GetAuthorQuery(Id));
            return Ok(author);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IQueryable<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var author = await _mediator.Send(new GetAllAuthorQuery());
            return Ok(author);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] AuthorCreateDto author)
        {
            var id = await _mediator.Send(new CreateAuthorCommand(author.Name, author.Email, author.Description));
            return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelopeGuid(id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteAuthorCommand(id));
            return NoContent();
        }
    }
}
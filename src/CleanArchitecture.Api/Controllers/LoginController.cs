using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Login.Commands;
using CleanArchitecture.Application.Login.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class LoginController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost()]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] LoginDto loginDto)
        {
            var tokenDto = await _mediator.Send(new CreateLoginCommand(loginDto));
            return Ok(tokenDto);
        }

    }
}

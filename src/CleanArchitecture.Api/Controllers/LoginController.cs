using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Login.Commands;
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

        [HttpGet("{email}/{password}")]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string email, string password)
        {
            var tokenDto = await _mediator.Send(new CreateLoginQuery(email, password));
            return Ok(tokenDto);
        }

    }
}

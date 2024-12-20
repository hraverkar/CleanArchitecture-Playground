using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Login.Command;
using CleanArchitecture.Application.Login.Commands;
using CleanArchitecture.Application.Login.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [AllowAnonymous]
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

        [HttpPost()]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] LoginDto loginDto )
        {
            var tokenDto = await _mediator.Send(new LoginCommand(loginDto));
            return Ok(tokenDto);
        }

    }
}

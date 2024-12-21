using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Login.Command;
using CleanArchitecture.Application.Login.Models;
using CleanArchitecture.Application.Logout.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class LogoutController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("logout")]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] string AccessToken)
        {
            var tokenDto = await _mediator.Send(new LogoutCommand(AccessToken));
            return Ok(tokenDto);
        }
    }
}
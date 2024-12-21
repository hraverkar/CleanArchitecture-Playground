using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Api.Infrastructure.Attributes;
using CleanArchitecture.Application.Authors.Commands;
using CleanArchitecture.Application.Login.Command;
using CleanArchitecture.Application.Login.Models;
using CleanArchitecture.Application.Logout.Commands;
using CleanArchitecture.Application.RegisterUsers.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymousMiddleware]
        [HttpPost("login")]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] LoginDto loginDto)
        {
            var tokenDto = await _mediator.Send(new LoginCommand(loginDto));
            return Ok(tokenDto);
        }

        [AllowAnonymousMiddleware]
        [HttpPost("register")]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] RegisterUserRequestDto registerDto)
        {
            var SuccessfullUserCreation = await _mediator.Send(new RegisterUserCreateCommand(registerDto));
            return Ok(SuccessfullUserCreation);
        }

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status200OK)]
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

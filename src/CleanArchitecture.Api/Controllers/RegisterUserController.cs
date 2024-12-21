using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Authors.Commands;
using CleanArchitecture.Application.RegisterUsers.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class RegisterUserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost()]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] RegisterUserRequestDto registerDto)
        {
            var tokenDto = await _mediator.Send(new RegisterUserCreateCommand(registerDto));
            return Ok(tokenDto);
        }

    }
}

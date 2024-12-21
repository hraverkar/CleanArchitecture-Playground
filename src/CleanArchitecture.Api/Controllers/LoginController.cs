﻿using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Login.Command;
using CleanArchitecture.Application.Login.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class LoginController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

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

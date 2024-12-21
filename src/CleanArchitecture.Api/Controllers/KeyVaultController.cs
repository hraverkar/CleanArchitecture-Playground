using CleanArchitecture.Application.KeyVault;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public sealed class KeyVaultController(IMediator mediator, IKeyVaultManager secretManager) : ControllerBase
    {
        private readonly IKeyVaultManager _secretManager = secretManager;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string secretName)
        {
            try
            {
                if (string.IsNullOrEmpty(secretName))
                {
                    return BadRequest();
                }

                string secretValue = await _secretManager.GetSecret(secretName);
                if (!string.IsNullOrEmpty(secretValue))
                {
                    return Ok(secretValue);
                }
                else
                {
                    return NotFound("Secret key not found.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Error: Unable to read secret");
            }
        }
    }
}

using CleanArchitecture.Api.Infrastructure.Attributes;
using CleanArchitecture.Application.Locations.Models;
using CleanArchitecture.Application.Locations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [AllowAnonymousMiddleware]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LocationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var locations = await _mediator.Send(new GetLocationsQuery());
            return Ok(locations);
        }
    }
}
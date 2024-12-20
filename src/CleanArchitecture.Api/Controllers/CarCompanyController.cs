using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Abstractions.Services;
using CleanArchitecture.Application.Authors.Commands;
using CleanArchitecture.Application.CarCompnies.Commands;
using CleanArchitecture.Application.CarCompnies.Models;
using CleanArchitecture.Application.CarCompnies.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class CarCompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmailNotificationService _emailNotificationService;

        public CarCompanyController(IMediator mediator, IEmailNotificationService emailNotificationService)
        {
            _mediator = mediator;
            _emailNotificationService = emailNotificationService;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<CarCompaniesDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var carCompany = await _mediator.Send(new GetCarCompanyQuery());
            return Ok(carCompany);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CreateCarCompaniesDto carCompaniesDto)
        {
            var id = await _mediator.Send(new CreateCarCompanyCommand(carCompaniesDto.CarManufactureName));
            return CreatedAtAction(nameof(Get), new { id }, new CreatedResultEnvelopeGuid(id));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<CarCompaniesDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var carCompany = await _mediator.Send(new GetCarCompanyByIdQuery(id));
            return Ok(carCompany);
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

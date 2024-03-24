using CleanArchitecture.Api.Infrastructure.ActionResults;
using CleanArchitecture.Application.Email_Notification.Commands;
using CleanArchitecture.Application.Email_Notification.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class SendEmailNotificationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("sendEmail")]
        [ProducesResponseType(typeof(CreatedResultEnvelope), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PostEmailNotification([FromBody] EmailNotificationRequestDto emailNotification)
        {
            if (emailNotification == null)
            {
                return Problem("Email Notification is null.");
            }

            var id = await _mediator.Send(new EmailNotificationCommand(emailNotification));
            return Ok(new { Value = id });

        }

        //[HttpPost("bulk-sendEmail")]
        //public async Task<IActionResult> PostBulkEmailNotification([FromBody] FileDto file)
        //{
        //    if (file == null)
        //    {
        //        return Problem("Email Notification is null.");
        //    }
        //    var command = new SendBulkEmailNotificationCommand(file.FileName, file.FileData, null);
        //    var result = await _mediator.Send(command);
        //    return Ok(new { Value = result });

        //}

        //[HttpGet("donwload-bulk-email-template")]
        //public async Task<IActionResult> DownloadEmailTemplate(string FileName)
        //{
        //    var query = new GetTemplateQuery(FileName);
        //    var result = await _mediator.Send(query);
        //    const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    var fileResult = new FileContentResult(result, contentType)
        //    {
        //        FileDownloadName = FileName
        //    };
        //    return Ok(fileResult);
        //}

    }
}

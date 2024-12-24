using CleanArchitecture.Core.IntegrationEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BuildingBlocks.Services.Core.IntegrationEventHandlers
{
    public class SampleAutomationEventHandler : IRequestHandler<SampleAutomationEvent>
    {
        private readonly ILogger<SampleAutomationEventHandler> _logger;

        public SampleAutomationEventHandler(ILogger<SampleAutomationEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(SampleAutomationEvent request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling SampleAutomationEvent with Id: {request.Id} and InitiatedBy: {request.InitiatedBy}");
            return Task.CompletedTask;
        }
    }
}

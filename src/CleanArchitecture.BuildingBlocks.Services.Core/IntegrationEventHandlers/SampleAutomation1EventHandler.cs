using CleanArchitecture.Core.IntegrationEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BuildingBlocks.Services.Core.IntegrationEventHandlers
{
    public class SampleAutomation1EventHandler : IRequestHandler<SampleAutomation1Event>
    {
        private readonly ILogger<SampleAutomation1EventHandler> _logger;

        public SampleAutomation1EventHandler(ILogger<SampleAutomation1EventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(SampleAutomation1Event request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling SampleAutomationEvent with Id: {request.Id} and InitiatedBy: {request.InitiatedBy}");
            return Task.CompletedTask;
        }
    }
}

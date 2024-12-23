using CleanArchitecture.Core.IntegrationEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BuildingBlocks.Services.Core.IntegrationEventHandlers
{
    public class TaxAutomationEventHandler : IRequestHandler<TaxAutomationEvent>
    {
        private readonly ILogger<TaxAutomationEventHandler> _logger;

        public TaxAutomationEventHandler(ILogger<TaxAutomationEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(TaxAutomationEvent request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling TaxAutomationEvent with Id: {request.Id} and InitiatedBy: {request.InitiatedBy}");
            return Task.CompletedTask;
        }
    }
}

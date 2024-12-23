using CleanArchitecture.BuildingBlocks.EventBus;
using CleanArchitecture.BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BuildingBlocks.Services.Core
{
    public class EventService<T> : BackgroundService where T : IntegrationEvent
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<EventService<T>> _logger;

        public EventService(IEventBus eventBus, ILogger<EventService<T>> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation($"Registering events for {typeof(T)} service");
                _eventBus.Subscribe<T>();
                _logger.LogInformation($"Registration complete. Listening for events : {typeof(T)} events");
                 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the event");
                throw;
            }
        }
    }
}

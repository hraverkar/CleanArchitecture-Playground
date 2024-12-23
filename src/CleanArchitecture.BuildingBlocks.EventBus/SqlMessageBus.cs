using CleanArchitecture.BuildingBlocks.EventBus.Abstractions;
using CleanArchitecture.BuildingBlocks.EventBus.Interfaces;
using CleanArchitecture.BuildingBlocks.EventBus.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.BuildingBlocks.EventBus
{
    public class SqlMessageBus : EventProcessor, IEventBus
    {
        private readonly ILogger<ServiceBus> _logger;
        private readonly IQueueRepository _queueRepository;
        public SqlMessageBus(IServiceProvider serviceProvider, ILogger<ServiceBus> logger, IQueueRepository repository) : base(serviceProvider)
        {
            _logger = logger;
            _queueRepository = repository;
        }

        public async Task PublishAsync<T>(T @event, int? messageDelayTimeSeconds = null) where T : IntegrationEvent
        {
            await _queueRepository.InsertAsync(@event);
        }

        public void Subscribe<T>() where T : IntegrationEvent
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        var @event = await _queueRepository.GetNextAsync<T>();
                        if (@event != null)
                        {
                            _logger.LogInformation("Fetched next item to process from SqlMessageBus");
                            await ProcessEvent(@event.MessageData);
                            await _queueRepository.CompleteAsync(@event);
                        }
                        else
                        {
                            _logger.LogDebug("No new item in SqlMessageBus");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing message from SqlMessageBus");
                        throw;
                    }
                    Thread.Sleep(10000);
                }
            }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }
    }
}

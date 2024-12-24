using CleanArchitecture.BuildingBlocks.EventBus.Abstractions;
using CleanArchitecture.BuildingBlocks.EventBus.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace CleanArchitecture.BuildingBlocks.EventBus
{
    public class ServiceBus : EventProcessor, IEventBus
    {
        private readonly ILogger<ServiceBus> _logger;
        private readonly string ConnectionString = "";
        public ServiceBus(IServiceProvider serviceProvider, ILogger<ServiceBus> logger) : base(serviceProvider)
        {
            _logger = logger;
        }

        public async Task PublishAsync<T>(T @event, int? messageDelayTimeSeconds = null) where T : IntegrationEvent
        {
            var topicName = GetTopicName<T>();
            var client = new TopicClient(ConnectionString, topicName);
            var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body =
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event))
            };

            if (messageDelayTimeSeconds != null)
            {
                message.ScheduledEnqueueTimeUtc = DateTime.UtcNow.AddSeconds((double)messageDelayTimeSeconds);
            }
            await client.SendAsync(message);
        }

        private string GetTopicName<T>() where T : IntegrationEvent
        {
            var topicName = typeof(T).Name.ToString().ToLower();
            return topicName;
        }

        public void Subscribe<T>() where T : IntegrationEvent
        {
            var topicName = GetTopicName<T>();
            var subscriptionName = GetSubscriptionName<T>();
            var client = new SubscriptionClient(ConnectionString, topicName, subscriptionName);
            client.RegisterMessageHandler(async (message, cancellationToken) =>
            {
                var messageData = Encoding.UTF8.GetString(message.Body);
                var @event = JsonConvert.DeserializeObject<T>(messageData);
                @event.DeliveryCount = message.SystemProperties.DeliveryCount;
                await ProcessEvent(@event);
                await client.CompleteAsync(message.SystemProperties.LockToken);

            }, new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            }); ;
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var ex = exceptionReceivedEventArgs.Exception;
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            _logger.LogError(ex, "ERROR handling message: {ExceptionMessage} - Context: {@ExceptionContext}", ex.Message, context);
            return Task.CompletedTask;
        }

        private string GetSubscriptionName<T>() where T : IntegrationEvent
        {
            var topicName = typeof(T).Name.ToString().ToLower();
            return $"{topicName}subscription";
        }
    }
}

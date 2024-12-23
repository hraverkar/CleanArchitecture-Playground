namespace CleanArchitecture.BuildingBlocks.EventBus.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event, int? messageDelayTimeSeconds = null) where T : IntegrationEvent;
        void Subscribe<T>() where T : IntegrationEvent;
    }
}

using CleanArchitecture.BuildingBlocks.EventBus.Models;

namespace CleanArchitecture.BuildingBlocks.EventBus.Interfaces.Repositories
{
    public interface IQueueRepository
    {
        Task InsertAsync<T>(T message) where T : IntegrationEvent;
        Task<Message<T>> GetNextAsync<T>() where T : IntegrationEvent;
        Task CompleteAsync<T>(Message<T> message) where T : IntegrationEvent;
    }
}

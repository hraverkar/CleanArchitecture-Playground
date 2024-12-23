using MediatR;

namespace CleanArchitecture.BuildingBlocks.EventBus
{
    public abstract class IntegrationEvent : IRequest
    {
        public readonly DateTime CreateDate;
        public int DeliveryCount { get; set; }
        public IntegrationEvent()
        {
            CreateDate = DateTime.UtcNow;
        }
    }
}

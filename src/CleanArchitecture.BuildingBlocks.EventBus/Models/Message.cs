namespace CleanArchitecture.BuildingBlocks.EventBus.Models
{
    public class Message<T> where T : IntegrationEvent
    {
        public Message(Guid messageId, T messageData)
        {
            MessageId = messageId;
            MessageData = messageData;
        }

        public Guid MessageId { get; set; }
        public T MessageData { get; set; }
    }
}

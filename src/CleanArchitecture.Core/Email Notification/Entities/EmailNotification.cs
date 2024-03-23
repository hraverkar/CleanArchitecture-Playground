using CleanArchitecture.Core.Abstractions.Entities;

namespace CleanArchitecture.Core.Email_Notification.Entities
{
    public sealed class EmailNotification : AggregateRoot
    {
        public string ToEmailUserName { get; set; }
        public string ToEmailAddress { get; set; }
        public string FromEmailUserName { get; set; }
        public string FromEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public bool IsSuccessed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        private EmailNotification(string toEmailUserName, string toEmailAddress, string fromEmailUserName,
            string fromEmailAddress, string subject, string body, bool isSuccessed, DateTime createdAt)
        {
            ToEmailAddress = toEmailAddress;
            FromEmailUserName = fromEmailUserName;
            FromEmailAddress = fromEmailAddress;
            Subject = subject;
            Body = body;
            ToEmailAddress = toEmailUserName;
            IsSuccessed = isSuccessed;
            CreatedAt = createdAt;
        }

#pragma warning disable CS8618 // this is needed for the ORM for serializing Value Objects
        private EmailNotification()
#pragma warning restore CS8618
        {

        }

        public static EmailNotification Create(string toEmailUserName, string toEmailAddress, string fromEmailUserName,
            string fromEmailAddress, string subject, string body, bool isSuccessed, DateTime createdAt)
        {
            return new EmailNotification(toEmailUserName, toEmailAddress, fromEmailUserName,
            fromEmailAddress, subject, body, isSuccessed, createdAt);
        }
    }
}

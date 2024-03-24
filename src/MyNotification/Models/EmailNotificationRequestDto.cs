namespace MyNotification.Models
{
    public class EmailNotificationRequestDto
    {
        public string ToEmailUserName { get; set; }
        public string ToEmailAddress { get; set; }
        public string FromEmailUserName { get; set; }
        public string FromEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string? Attachment { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}

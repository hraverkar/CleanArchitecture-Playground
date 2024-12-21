using MimeKit;

namespace MyNotification.Models
{
    public class EmailNotificationRequestDto
    {
        public string ToEmailUserName { get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

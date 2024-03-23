using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Email_Notification.Models
{
    public sealed class EmailNotificationRequestDto
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

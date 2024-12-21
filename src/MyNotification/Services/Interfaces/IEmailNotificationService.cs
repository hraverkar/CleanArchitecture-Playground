using MyNotification.Models;

namespace MyNotification.Services.Interfaces
{
    public interface IEmailNotificationService
    {
        Task<string> EmailNotificationAlertAsync(EmailNotificationRequestDto emailNotificationRequestDto);
    }
}

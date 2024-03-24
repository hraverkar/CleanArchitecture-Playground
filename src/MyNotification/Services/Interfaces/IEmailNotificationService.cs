using MyNotification.Models;

namespace MyNotification.Services.Interfaces
{
    public interface IEmailNotificationService
    {
        Task<bool> EmailNotificationAlertAsync(EmailNotificationRequestDto emailNotificationRequestDto);
    }
}

using CleanArchitecture.Application.Email_Notification.Models;

namespace CleanArchitecture.Application.Abstractions.Services
{
    public interface IEmailNotificationService
    {
        Task<bool> EmailNotificationAlertAsync(EmailNotificationRequestDto emailNotificationRequestDto);
    }
}

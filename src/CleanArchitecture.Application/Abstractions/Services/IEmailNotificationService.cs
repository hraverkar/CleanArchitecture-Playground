namespace CleanArchitecture.Application.Abstractions.Services
{
    public interface IEmailNotificationService
    {
        Task EmailNotificationAlertAsync(string userEmail, string userName, string Subject);
    }
}

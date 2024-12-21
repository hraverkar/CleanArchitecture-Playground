using CleanArchitecture.Application.Abstractions.Services;
using CleanArchitecture.Application.Email_Notification.Models;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CleanArchitecture.Infrastructure.Services
{
    public sealed class EmailNotificationService(ILogger<EmailNotificationService> logger, IConfiguration configuration) : IEmailNotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger = logger;
        private readonly MailSettings _mailSettings = configuration?.GetSection("MailSettings").Get<MailSettings>();

        public async Task<bool> EmailNotificationAlertAsync(EmailNotificationRequestDto emailNotificationRequestDto)
        {
            try
            {
                var mail = new MimeMessage
                {
                    From = { new MailboxAddress(_mailSettings.DisplayName, _mailSettings.From) },
                    Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.From),
                    To = { new MailboxAddress(emailNotificationRequestDto.ToEmailUserName, emailNotificationRequestDto.ToEmailAddress) },
                    Subject = emailNotificationRequestDto.Subject
                };

                var body = new BodyBuilder { HtmlBody = emailNotificationRequestDto.Body };
                mail.Body = body.ToMessageBody();

                using var smtpClient = new SmtpClient();

                var secureSocketOptions = _mailSettings.UseSSL
                    ? SecureSocketOptions.SslOnConnect
                    : (_mailSettings.UseStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);

                await smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, secureSocketOptions);
                await smtpClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
                await smtpClient.SendAsync(mail);
                await smtpClient.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

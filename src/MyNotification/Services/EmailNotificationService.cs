using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MyNotification.Models;
using MyNotification.Services.Interfaces;

namespace MyNotification.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IConfiguration _configuration;

        public EmailNotificationService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<bool> EmailNotificationAlertAsync(EmailNotificationRequestDto emailNotificationRequestDto)
        {
            var mailSettings = _configuration.GetSection("MailSettings").Get<MailSettings>();
            try
            {
                var mail = new MimeMessage
                {
                    From = { new MailboxAddress(mailSettings.DisplayName, mailSettings.From) },
                    Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.From),
                    To = { new MailboxAddress(emailNotificationRequestDto.ToEmailUserName, emailNotificationRequestDto.ToEmailAddress) },
                    Subject = emailNotificationRequestDto.Subject
                };

                var body = new BodyBuilder { HtmlBody = emailNotificationRequestDto.Body };
                mail.Body = body.ToMessageBody();

                using var smtpClient = new SmtpClient();

                var secureSocketOptions = mailSettings.UseSSL
                    ? SecureSocketOptions.SslOnConnect
                    : (mailSettings.UseStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);

                await smtpClient.ConnectAsync(mailSettings.Host, mailSettings.Port, secureSocketOptions);
                await smtpClient.AuthenticateAsync(mailSettings.UserName, mailSettings.Password);
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

using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Abstractions.Services;
using CleanArchitecture.Application.Email_Notification.Models;
using CleanArchitecture.Core.Email_Notification.Entities;

namespace CleanArchitecture.Application.Email_Notification.Commands
{
    public sealed record EmailNotificationCommand(EmailNotificationRequestDto EmailNotificationRequestDto) : CreateCommand;

    public sealed class EmailNotificationCommandHandler : CreateCommandHandler<EmailNotificationCommand>
    {
        private readonly IRepository<EmailNotification> _repository;
        private readonly IEmailNotificationService _emailNotificationService;

        public EmailNotificationCommandHandler(IRepository<EmailNotification> repository, IEmailNotificationService emailNotificationService,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _repository = repository;
            _emailNotificationService = emailNotificationService;
        }

        protected async override Task<string> HandleAsync(EmailNotificationCommand request)
        {
            try
            {
                bool isEmailSent = await _emailNotificationService.EmailNotificationAlertAsync(request.EmailNotificationRequestDto);
                var emailNotification = EmailNotification.Create(
                    request.EmailNotificationRequestDto.ToEmailUserName,
                    request.EmailNotificationRequestDto.ToEmailAddress,
                    request.EmailNotificationRequestDto.FromEmailUserName,
                    request.EmailNotificationRequestDto.FromEmailAddress,
                    request.EmailNotificationRequestDto.Subject,
                    request.EmailNotificationRequestDto.Body,
                    isEmailSent,
                    DateTime.Now);
                _repository.Insert(emailNotification);
                await UnitOfWork.CommitAsync();
                return "Email Sent !!";
            }
            catch (Exception ex)
            {
                return await Task.FromResult($"Email Send failed!! \r\n {ex}");
            }
        }
    }
}

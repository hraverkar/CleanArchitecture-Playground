using CleanArchitecture.Application.Abstractions.Commands;
using CleanArchitecture.Application.Abstractions.Repositories;
using CleanArchitecture.Application.Abstractions.Services;
using CleanArchitecture.Application.Email_Notification.Models;
using CleanArchitecture.BuildingBlocks.EventBus.Interfaces;
using CleanArchitecture.Core.Email_Notification.Entities;
using CleanArchitecture.Core.IntegrationEvents;

namespace CleanArchitecture.Application.Email_Notification.Commands
{
    public sealed record EmailNotificationCommand(EmailNotificationRequestDto EmailNotificationRequestDto) : CommandBase<string>;

    public sealed class EmailNotificationCommandHandler : CommandHandler<EmailNotificationCommand, string>
    {
        private readonly IRepository<EmailNotification> _repository;
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly IEventBus _eventBus;

        public EmailNotificationCommandHandler(IRepository<EmailNotification> repository, IEmailNotificationService emailNotificationService,
            IUnitOfWork unitOfWork, IEventBus eventBus) : base(unitOfWork)
        {
            _repository = repository;
            _emailNotificationService = emailNotificationService;
            _eventBus = eventBus;
        }
        protected override async Task<string> HandleAsync(EmailNotificationCommand request, CancellationToken cancellationToken)
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
                await _eventBus.PublishAsync(new TaxAutomationEvent(emailNotification.Id, emailNotification.ToEmailUserName));
                await UnitOfWork.CommitAsync();
                return await Task.FromResult("Email Sent !!");
            }
            catch (Exception ex)
            {
                return await Task.FromResult($"Email Send failed!! \r\n {ex}");
            }
        }
    }
}

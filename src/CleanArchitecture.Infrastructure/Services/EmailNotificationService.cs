using CleanArchitecture.Application.Abstractions.Services;
using CleanArchitecture.Core.Abstractions.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public sealed class EmailNotificationService : IEmailNotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;
        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            _logger = logger;
        }
        public Task EmailNotificationAlertAsync(string userEmail, string userName, string subject)
        {
            return Task.CompletedTask;
        }

    }
}

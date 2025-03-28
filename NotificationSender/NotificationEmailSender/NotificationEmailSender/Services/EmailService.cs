using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using NotificationEmailSender.Models;
using SnowPro.Shared.Contracts;

namespace NotificationEmailSender.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IFluentEmail _fluentEmail;

    public EmailService(ILogger<EmailService> logger, IFluentEmail fluentEmail)
    {
        _logger = logger;
        _fluentEmail = fluentEmail;
    }

    public async Task SendRegistrationEmailAsync(EmailNotificationMessageDto emailNotification)
    {
        _logger.LogInformation("Sending registration email to: {To}", emailNotification.Email);
        var model = new WelcomeEmailModel
        {
            UserName = emailNotification.Username,
            Message = emailNotification.Message
        };
             await _fluentEmail
            .To(emailNotification.Email)
            .Subject("Добро пожаловать в лыжную школу!")
            .UsingTemplateFromFile($"Templates/WelcomeEmail.cshtml", model)
            .SendAsync();
        _logger.LogInformation("Registration email sent to: {To}", emailNotification.Email);
    }
}
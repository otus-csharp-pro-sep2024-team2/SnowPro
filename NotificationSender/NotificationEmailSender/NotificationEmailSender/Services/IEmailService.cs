using System.Threading.Tasks;
using SnowPro.Shared.Contracts;

namespace NotificationEmailSender.Services
{
    public interface IEmailService
    {
        Task SendRegistrationEmailAsync(EmailNotificationMessageDto emailNotification);
    }
}


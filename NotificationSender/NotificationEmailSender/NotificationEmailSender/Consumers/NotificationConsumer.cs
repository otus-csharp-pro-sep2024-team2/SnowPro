using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SnowPro.Shared.Contracts;

namespace NotificationEmailSender.Consumers
{
    public class NotificationConsumer(ILogger<NotificationConsumer> logger) : IConsumer<EmailNotificationMessageDto>
    {
        public async Task Consume(ConsumeContext<EmailNotificationMessageDto> contextMq)
        {
            var message = contextMq.Message;
            logger.LogInformation($"Received notification message: {message.UserId}");
            await Task.Delay(1000);
            logger.LogInformation($"Sent email notification: {message.UserId}");
        }
    }
}
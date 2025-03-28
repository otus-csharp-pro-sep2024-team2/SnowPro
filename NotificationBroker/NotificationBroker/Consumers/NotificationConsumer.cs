using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SnowPro.Shared.Contracts;

namespace NotificationBroker.Consumers
{
    public class NotificationConsumer(ILogger<NotificationConsumer> logger) : IConsumer<NotificationMessageDto>
    {
        private ConsumeContext<NotificationMessageDto>? _contextMq;
        public async Task Consume(ConsumeContext<NotificationMessageDto> contextMq)
        {
            _contextMq = contextMq;
            var message = contextMq.Message;
            logger.LogInformation($"Received notification: {message.UserId} ({message.Type})");
            try
            {
                switch (message.Type)
                {
                    case "email":
                        await SendEmailNotificationAsync(message, contextMq);
                        break;
                    case "sms":
                        await SendSmsNotificationAsync(message, contextMq);
                        break;
                    case "telegram":
                        await SendTelegramNotificationAsync(message, contextMq);
                        break;
                    default:
                        logger.LogWarning($"Unknown notification type: {message.Type}");
                        break;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to process notification");
            }
        }

        private async Task SendEmailNotificationAsync(
            NotificationMessageDto message, 
            ConsumeContext<NotificationMessageDto> contextMq)
        {
            var msg =new EmailNotificationMessageDto()
            {
                UserId = message.UserId,
                Username = message.Username,
                Email = message.Email,
                Message = message.Message
            };
            // Publish to the exchange configured for this message type
            await contextMq.Publish(msg);
            logger.LogInformation($"Published {message.Type} message for {message.UserId}");
        }

        private async Task SendSmsNotificationAsync(
            NotificationMessageDto message,
            ConsumeContext<NotificationMessageDto> contextMq)
        {
            var msg =new SMSNotificationMessageDto()
            {
                UserId = message.UserId,
                Username = message.Username,
                Email = message.Email,
                Message = message.Message
            };
            await contextMq.Publish(msg);
            logger.LogInformation($"Published {message.Type} message for {message.UserId}");
        }

        private async Task SendTelegramNotificationAsync(
            NotificationMessageDto message,
            ConsumeContext<NotificationMessageDto> contextMq)
        {
            var msg =new TelegramNotificationMessageDto()
            {
                UserId = message.UserId,
                Username = message.Username,
                TelegramId = message.TelegramId,
                Message = message.Message
            };
            await contextMq.Publish(msg);
            logger.LogInformation($"Published {message.Type} message for {message.UserId}");
        }
    }
}

using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SnowPro.Shared.Contracts;

namespace NotificationTelegramSender.Consumers;

public class NotificationConsumer(ILogger<NotificationConsumer> logger) : IConsumer<TelegramNotificationMessageDto>
{
    public async Task Consume(ConsumeContext<TelegramNotificationMessageDto> contextMq)
    {
        var message = contextMq.Message;
        await Task.Delay(1000);
        //logger.LogInformation($"Received notification message: {message.UserId}");
        logger.LogInformation($"Sent Telegram notification: {message.UserId}");
    }
}
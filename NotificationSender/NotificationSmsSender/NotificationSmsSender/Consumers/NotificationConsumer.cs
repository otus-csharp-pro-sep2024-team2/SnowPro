using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using SnowPro.Shared.Contracts;

namespace NotificationSmsSender.Consumers;

public class NotificationConsumer(ILogger<NotificationConsumer> logger) : IConsumer<SMSNotificationMessageDto>
{
    public async Task Consume(ConsumeContext<SMSNotificationMessageDto> contextMq)
    {
        var message = contextMq.Message;
        await Task.Delay(1000);
        //logger.LogInformation($"Received notification message: {message.UserId}");
        logger.LogInformation($"Sent sms notification: {message.UserId}");
    }
}
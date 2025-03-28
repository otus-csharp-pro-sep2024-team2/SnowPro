using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using SnowPro.Shared.Contracts;

namespace AuthorizationService.Infrastructure.MessageBroker;

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> _logger;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;


    public MessageService(
        IPublishEndpoint publishEndpoint,
        ILogger<MessageService> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }
    public void PublishUser(UserDto user)
    {
        PublishToServices(user);
        PublishNotification(user);
    }

    private void PublishNotification(UserDto user)
    {
        var notificationMessageDto = _mapper.Map<NotificationMessageDto>(user);
        notificationMessageDto.Message = "Welcome! Your registration was successful.";

        var notificationTypes = new[] { "email", "sms", "telegram" };

        foreach (var type in notificationTypes)
        {
            notificationMessageDto.Type = type;
            _publishEndpoint.Publish(notificationMessageDto);
            _logger.LogInformation($"{type} notification message published");
        }
    }

    private async void PublishToServices(UserDto user)
    {
        var userRegisteredDto = _mapper.Map<UserRegisteredDto>(user);
        await _publishEndpoint.Publish(userRegisteredDto);
        _logger.LogInformation("User registered message published");
    }

    public async void Publish<T>(T message)
    {
        try
        {
            await _publishEndpoint.Publish(message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while publishing message");
        }
    }
}
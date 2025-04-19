using MassTransit;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.ClientProfileInfoContracts;
using ProfileService.Application.Contracts.InstructorProfileInfoContracts;
using ProfileService.Common.Enums;
using SnowPro.Shared.Contracts;

namespace ProfileService.API.Consumers;

public class UserRegisteredConsumer(
    ILogger<UserRegisteredConsumer> logger,
    IClientProfileInfoServiceApp clientProfileInfoService,
    IInstructorProfileInfoServiceApp istructorProfileInfoService
) : IConsumer<UserRegisteredDto>
{
    public async Task Consume(ConsumeContext<UserRegisteredDto> context)
    {
        UserRegisteredDto? message = context.Message;
        logger.LogInformation($"Received: {message.UserId} ({message.RoleName})");
        try
        {
            switch (message.RoleName)
            {
                case nameof(ProfileType.Client):
                    await CreateClientProfileInfoAsync(message);
                    break;
                case nameof(ProfileType.Instructor):
                    await CreateInstructorProfileInfoAsync(message);
                    break;
                default:
                    throw new ArgumentException(nameof(message.RoleName), $"Unknown role: {message.RoleName}");
            };
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to process the creation of the user profile after registration");
        }
    }

    /// <summary>
    /// Создание профиля клиента после регистрации пользователя
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private async Task CreateClientProfileInfoAsync(UserRegisteredDto message)
    {
        await clientProfileInfoService.CreateAsync(message.UserId,
                new CreatingClientProfileInfoDto()
                {
                    Name = message.FirstName,
                    Surname = message.LastName,
                    PhoneNumber = message.PhoneNumber,
                    Email = message.Email,
                    Username = message.Username,
                    TelegramName = message.TelegramId
                });
    }

    /// <summary>
    /// Создание профиля инструктора после регистрации пользователя
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private async Task CreateInstructorProfileInfoAsync(UserRegisteredDto message)
    {
        await istructorProfileInfoService.CreateAsync(message.UserId,
                new CreatingInstructorProfileInfoDto()
                {
                    Name = message.FirstName,
                    Surname = message.LastName,
                    PhoneNumber = message.PhoneNumber,
                    Email = message.Email,
                    Username = message.Username,
                    TelegramName = message.TelegramId
                });
    }
}

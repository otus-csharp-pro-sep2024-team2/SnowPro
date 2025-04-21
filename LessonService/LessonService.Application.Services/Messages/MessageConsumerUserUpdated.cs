using AutoMapper;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Infrastructure.EF;
using MassTransit;
using Microsoft.Extensions.Logging;
using SnowPro.Shared.Contracts;

namespace LessonService.Application.Services.Messages
{
    public class MessageConsumerUserUpdated(
        ILogger<MessageConsumerUserUpdated> logger,
        AppDbContext context,
        IMapper mapper) : IConsumer<SharedProfileInfoDto>
    {
        private ConsumeContext<SharedProfileInfoDto>? _contextMq;

        public async Task Consume(ConsumeContext<SharedProfileInfoDto> contextMq)
        {
            _contextMq = contextMq;
            var message = contextMq.Message;
            logger.LogInformation($"Received profile's changes: {message.UserId} ({message.Surname} {message.Name})");
            try
            {
                var person = await context.Persons.FindAsync(message.UserId);
                if (person is null)
                {
                    logger.LogWarning($"User {message.UserId} not exists in the database.");
                    throw new Exception($"User {message.UserId} not exists in the database.");
                }
                person.FirstName = message.Name;
                person.LastName = message.Surname;
                person.PhoneNumber = message.PhoneNumber;
                person.Email = message.Email;
                person.TelegramId = message.TelegramName;

                await context.SaveChangesAsync();
                logger.LogInformation($"User {message.UserId} ({message.Surname} {message.Name}) changed.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to process notification");
            }
        }
    }
}

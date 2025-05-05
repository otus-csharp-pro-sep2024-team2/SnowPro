using AutoMapper;
using LessonService.Domain.Entities.Base;
using LessonService.Infrastructure.EF;
using LessonService.Interfaces;
using MassTransit;
using Microsoft.Extensions.Hosting;
using SnowPro.Shared.Contracts;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Application.Services.Messages
{
    public class MessageConsumerUserUpdated(
        IServiceLogger logger,
        IUnitOfWork unitOfWork
        ) : IConsumer<SharedProfileInfoDto>
    {
        public async Task Consume(ConsumeContext<SharedProfileInfoDto> contextMq)
        {
            var message = contextMq.Message;
            logger.LogInformation($"Received profile's changes: {message.UserId} ({message.Surname} {message.Name})");
            try
            {
                var person = await unitOfWork.Lessons.GetPersonByIdAsync<Person>(message.UserId, contextMq.CancellationToken);
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

                await unitOfWork.SaveChangesAsync(contextMq.CancellationToken);
                logger.LogInformation($"User {message.UserId} ({message.Surname} {message.Name}) changed.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to process notification");
            }
        }
    }
}

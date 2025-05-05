using AutoMapper;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Interfaces;
using MassTransit;
using SnowPro.Shared.Contracts;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.Application.Services.Messages
{
    public class MessageConsumerUserRegistered(
        IServiceLogger logger,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IConsumer<UserRegisteredDto>
    {
        public async Task Consume(ConsumeContext<UserRegisteredDto> contextMq)
        {
            var message = contextMq.Message;
            logger.LogInformation($"Received new user registration: {message.UserId} ({message.Username})");
            if ( message.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                logger.LogWarning($"User {message.UserId} is an admin, skipping.");
                return;
            }
            logger.LogInformation($"Processing new user registration: {message.UserId} ({message.Username})");
            try
            {
                var person = await unitOfWork.Lessons.GetPersonByIdAsync<Person>(message.UserId, contextMq.CancellationToken);
                if (person is not null)
                {
                    logger.LogWarning($"User {message.UserId} already exists in the database.");
                    throw new Exception($"User {message.UserId} already exists in the database.");
                }
                if (message.RoleName.Equals("Client", StringComparison.OrdinalIgnoreCase))
                {
                    person = mapper.Map<Student>(message);
                }
                else if (message.RoleName.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
                {
                    person = mapper.Map<Instructor>(message);
                }
                else
                {
                    logger.LogWarning($"Unknown role: {message.RoleName}");
                    throw new Exception($"Unknown role: {message.RoleName}");
                };
                await unitOfWork.Lessons.AddPersonAsync(person);
                logger.LogInformation($"User {message.UserId} ({message.Username}) added to the database.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to process notification");
            }
        }
    }
}

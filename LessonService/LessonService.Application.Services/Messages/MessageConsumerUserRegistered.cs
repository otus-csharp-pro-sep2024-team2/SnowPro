using AutoMapper;
using LessonService.Domain.Entities;
using LessonService.Domain.Entities.Base;
using LessonService.Infrastructure.EF;
using MassTransit;
using Microsoft.Extensions.Logging;
using SnowPro.Shared.Contracts;

namespace LessonService.Application.Services.Messages
{
    public class MessageConsumerUserRegistered(ILogger<MessageConsumerUserRegistered> logger, AppDbContext context, IMapper mapper) : IConsumer<UserRegisteredDto>
    {
        private ConsumeContext<UserRegisteredDto>? _contextMq;

        public async Task Consume(ConsumeContext<UserRegisteredDto> contextMq)
        {
            _contextMq = contextMq;
            var message = contextMq.Message;
            logger.LogInformation($"Received new user registration: {message.UserId} ({message.Username})");
            if (message.RoleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                logger.LogWarning($"User {message.UserId} is an admin, skipping.");
                return;
            }
            logger.LogInformation($"Processing new user registration: {message.UserId} ({message.Username})");
            try
            {
                var person = await context.Persons.FindAsync(message.UserId);
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
                await context.Persons.AddAsync(person);
                await context.SaveChangesAsync();
                logger.LogInformation($"User {message.UserId} ({message.Username}) added to the database.");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to process notification");
            }
        }
    }
}

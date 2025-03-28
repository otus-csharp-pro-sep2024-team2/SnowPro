using AuthorizationService.Infrastructure;
using AuthorizationService.Infrastructure.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Options;
using SnowPro.Shared.Contracts;

namespace AuthorizationService.API.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitConfig = context.GetRequiredService<IOptions<RabbitConfig>>().Value;
                cfg.Host(rabbitConfig.Host, h =>
                {
                    h.Username(rabbitConfig.Username);
                    h.Password(rabbitConfig.Password);
                });

                // Publish in Exchange (Fanout)
                cfg.Message<NotificationMessageDto>(e => e.SetEntityName(rabbitConfig.ExchangeName));
            });
        });
        return services;
    }    
}
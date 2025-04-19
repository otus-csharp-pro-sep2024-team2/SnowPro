using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProfileService.API.Consumers;
using ProfileService.API.Mapping;
using ProfileService.API.Models;
using ProfileService.API.Settings;
using ProfileService.Application.Abstractions;
using ProfileService.Application.Contracts.ClientProfileInfoContracts;
using ProfileService.Application.Contracts.InstructorProfileInfoContracts;
using ProfileService.Application.Contracts.ProfileInfoContracts;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Application.Services;
using ProfileService.Infrastructure.EntityFramework;
using ProfileService.Infrastructure.Repositories.Implementations;
using SnowPro.Shared.Contracts;
using System.Text;
using ServicesMapping = ProfileService.Application.Services.Mapping;

namespace ProfileService.API;

/// <summary>
/// Регистратор сервиса.
/// </summary>
public static class Registrar
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        ApplicationSettings? applicationSettings = configuration.Get<ApplicationSettings>();
        services.AddSingleton(applicationSettings)
                .AddSingleton((IConfigurationRoot)configuration)
                .InstallServices()
                .ConfigureContext(applicationSettings.ConnectionString)
                .InstallRepositories();
        return services;
    }

    private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddTransient<IAchievementServiceApp, AchievementServiceApp>()
            .AddTransient<IClientProfileInfoServiceApp, ClientProfileInfoServiceApp>()
            .AddTransient<IFileAchievementServiceApp, FileAchievementServiceApp>()
            .AddTransient<IInstructorProfileInfoServiceApp, InstructorProfileInfoServiceApp>()
            .AddTransient<ILevelTrainingServiceApp, LevelTrainingServiceApp>()
            .AddTransient<IPositionServiceApp, PositionServiceApp>()
            .AddTransient<IProfileInfoServiceApp, ProfileInfoServiceApp>()
            .AddTransient<ITypeSportEquipmentServiceApp, TypeSportEquipmentServiceApp>()
            .AddTransient<IUnitOfWork, UnitOfWork>()
            ;
        return serviceCollection;
    }

    private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddTransient<IAchievementRepository, AchievementRepository>()
            .AddTransient<IClientProfileInfoRepository, ClientProfileInfoRepository>()
            .AddTransient<IFileAchievementRepository, FileAchievementRepository>()
            .AddTransient<IInstructorProfileInfoRepository, InstructorProfileInfoRepository>()
            .AddTransient<ILevelTrainingRepository, LevelTrainingRepository>()
            .AddTransient<IPositionRepository, PositionRepository>()
            .AddTransient<IProfileInfoRepository, ProfileInfoRepository>()
            .AddTransient<ITypeSportEquipmentRepository, TypeSportEquipmentRepository>()
            .AddTransient<IUnitOfWork, UnitOfWork>();
        ;

        return serviceCollection;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
        return serviceCollection;
    }

    private static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AchievementMappingsProfile>();
            cfg.AddProfile<ServicesMapping.AchievementMappingsProfile>();

            cfg.AddProfile<ClientProfileInfoMappingsProfile>();
            cfg.AddProfile<ServicesMapping.ClientProfileInfoMappingsProfile>();

            cfg.AddProfile<FileAchievementMappingsProfile>();
            cfg.AddProfile<ServicesMapping.FileAchievementMappingsProfile>();

            cfg.AddProfile<InstructorProfileInfoMappingsProfile>();
            cfg.AddProfile<ServicesMapping.InstructorProfileInfoMappingsProfile>();

            cfg.AddProfile<LevelTrainingMappingsProfile>();
            cfg.AddProfile<ServicesMapping.LevelTrainingMappingsProfile>();

            cfg.AddProfile<PositionMappingsProfile>();
            cfg.AddProfile<ServicesMapping.PositionMappingsProfile>();

            cfg.AddProfile<ProfileInfoMappingsProfile>();
            cfg.AddProfile<ServicesMapping.ProfileInfoMappingsProfile>();

            cfg.AddProfile<TypeSportEquipmentProfileInfoMappingsProfile>();
            cfg.AddProfile<ServicesMapping.TypeSportEquipmentProfileInfoMappingsProfile>();

            cfg.AddProfile<TypeSportEquipmentMappingsProfile>();
            cfg.AddProfile<ServicesMapping.TypeSportEquipmentMappingsProfile>();
        });
        configuration.AssertConfigurationIsValid();
        return configuration;
    }

    public static IServiceCollection AddMassTransitRmq(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddMassTransit(x =>
        {

            x.AddConsumer<UserRegisteredConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var rmqSettings = configuration.Get<ApplicationSettings>().RabbitMqSettings;
                cfg.Host(rmqSettings.Host,
                    h =>
                    {
                        h.Username(rmqSettings.Username);
                        h.Password(rmqSettings.Password);
                    });

                cfg.ReceiveEndpoint(rmqSettings.QueueName, e =>
                {
                    e.Bind(rmqSettings.ExchangeName, x => x.ExchangeType = "fanout");
                    e.ConfigureConsumer<UserRegisteredConsumer>(context);
                });

                cfg.Message<SharedProfileInfoDto>(x => x.SetEntityName("profile-exchange"));
            });
        });
    }

    public static IServiceCollection AddAuthJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<AuthJwt>();
        ArgumentNullException.ThrowIfNull(jwtSettings);
        ArgumentNullException.ThrowIfNull(jwtSettings.Issuer);
        ArgumentNullException.ThrowIfNull(jwtSettings.Audience);
        ArgumentNullException.ThrowIfNull(jwtSettings.Key);

        services.Configure<AuthJwt>(configuration.GetSection("JwtSettings"))
            .AddAuthorization()
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
        return services;
    }
}

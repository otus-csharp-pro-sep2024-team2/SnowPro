using System.Text;
using LessonService.Application.Services;
using LessonService.Application.Services.Mapping;
using LessonService.Application.Services.Messages;
using LessonService.Commands.Commands;
using LessonService.Commands.Requests.Commands;
using LessonService.Domain.Models.Lesson;
using LessonService.Domain.Models.System;
using LessonService.Infrastructure.EF;
using LessonService.Infrastructure.Repositories;
using LessonService.Interfaces;
using LessonService.WebApi.Exception;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SnowPro.Shared.Contracts;
using SnowPro.Shared.ServiceLogger;

namespace LessonService.WebApi.Extentions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        var dbConnection = configuration.GetConnectionString("DefaultConnection");
        ArgumentNullException.ThrowIfNull(dbConnection);
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(dbConnection, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("LessonService.Infrastructure.EF");
            });

            // Disable extra logging
            options.LogTo(_ => { }, LogLevel.Warning);
        }); 
        services.AddLogging()
            .AddSingleton<IServiceLogger, ServiceLogger>()
            .AddScoped<IRequestHandler<RemoveInstructorCommand, ApiResponse<LessonModel>>,
                RemoveInstructorCommandHandler>()
            .AddScoped<ILessonRepository, LessonRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddAuthJwt(configuration)
            .AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            })
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateLessonCommandHandler).Assembly))
            .AddAutoMapper(typeof(Program), typeof(LessonMapping))
            .AddRabbit(configuration)
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                        { Title = "LessonServiceApi", Version = "v1", Description = "SnowPro LessonService API" });
            });
        return services;
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

    public static IServiceCollection AddRabbit(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitConfig>(configuration.GetSection("RabbitMqSettings"))
            .AddScoped<IMessageService, MessageService>()
            .AddMassTransit(x =>
            {
                x.AddConsumer<MessageConsumerUserRegistered>();
                x.AddConsumer<MessageConsumerUserUpdated>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitConfig = context.GetRequiredService<IOptions<RabbitConfig>>().Value;
                    cfg.Host(rabbitConfig.Host, h =>
                    {
                        h.Username(rabbitConfig.Username);
                        h.Password(rabbitConfig.Password);
                    });
                    cfg.ReceiveEndpoint("lesson-user-registered", e =>
                    {
                        e.Bind(rabbitConfig.ExchangeName, x => x.ExchangeType = "fanout");
                        e.ConfigureConsumer<MessageConsumerUserRegistered>(context);
                    });
                    
                    cfg.ReceiveEndpoint("lesson-profile-changed", e =>
                    {
                        e.Bind("profile-exchange", x => x.ExchangeType ="fanout");
                        e.ConfigureConsumer<MessageConsumerUserUpdated>(context);
                    });
                    cfg.Message<SharedProfileInfoDto>(x => x.SetEntityName("profile-exchange"));                    
                });
            });
        return services;
    }
}
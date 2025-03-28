using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationEmailSender.Extensions;
using NotificationEmailSender.Models;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
});

builder.ConfigureServices((context, services) =>
{
    services.Configure<RabbitConfig>(context.Configuration.GetSection("RabbitMqSettings"));
    services.AddLogging(cfg => cfg.AddConsole());
    services.AddRabbit();
});

var app = builder.Build();
var config = app.Services.GetRequiredService<Microsoft.Extensions.Options.IOptions<RabbitConfig>>().Value;
Console.WriteLine($"Host: {config.Host}");
Console.WriteLine($"username: {config.Username}");
Console.WriteLine($"password: {config.Password}");
Console.WriteLine($"Queue: {config.QueueName}");
Console.WriteLine($"Exchange: {config.ExchangeName}");
await app.RunAsync();

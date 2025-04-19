﻿namespace ProfileService.API.Settings;

public class RabbitMqSettings
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ExchangeName { get; set; }
    public string QueueName { get; set; }

}

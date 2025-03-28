namespace NotificationTelegramSender.Models;

public class RabbitConfig
{
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ExchangeName { get; set; }
    public string QueueName { get; set; }
    public string NotificationExchangeName { get; set; }
}
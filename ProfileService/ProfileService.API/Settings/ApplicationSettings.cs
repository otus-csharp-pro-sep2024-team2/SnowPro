namespace ProfileService.API.Settings;

public class ApplicationSettings
{
    public string ConnectionString { get; set; }

    public RabbitMqSettings RabbitMqSettings { get; set; }
}
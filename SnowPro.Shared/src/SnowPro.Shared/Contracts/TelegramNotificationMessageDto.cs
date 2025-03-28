namespace SnowPro.Shared.Contracts;

public class TelegramNotificationMessageDto
{
    public string Type { get; } = "telegram"; // email, sms, telegram, event, log, billing
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? TelegramId { get; set; }
    public string Message { get; set; } = string.Empty; // ����� �����������
}
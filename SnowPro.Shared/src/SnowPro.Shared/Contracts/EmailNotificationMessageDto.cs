namespace SnowPro.Shared.Contracts;

public class EmailNotificationMessageDto
{
    public string Type { get; } = "email"; // email, sms, telegram, event, log, billing
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty; // ����� �����������

}
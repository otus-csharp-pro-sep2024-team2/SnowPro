namespace SnowPro.Shared.Contracts;

public class NotificationMessageDto
{
    public string Type { get; set; } = string.Empty; 
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty; // Текст уведомления
    public string TelegramId { get; set; } = string.Empty;
}

namespace LessonService.Domain.Entities.Base;

public class Person(Guid id) : Entity<Guid>(id)
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public string? RoleName { get; set; } 
    public string? TelegramId { get; set; }
    public string Name => $"{FirstName} {LastName}"; 
    public Person() : this(Guid.NewGuid())
    {
    }
}

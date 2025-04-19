namespace ProfileService.Domain.Entities;

/// <summary>
/// Файл достижения
/// </summary>
public class FileAchievement: IEntity<int>
{
    public int Id { get; set; }

    public Guid FileId { get; set; }

    public int AchievementId { get; set; }

    public virtual Achievement Achievement { get; set; } = null!;
}
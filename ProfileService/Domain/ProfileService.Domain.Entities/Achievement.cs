namespace ProfileService.Domain.Entities;
/// <summary>
/// Достижения профиля.
/// </summary>
public class Achievement: IEntity<int>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Идентификатор профиля.
    /// </summary>
    public Guid ProfileInfoId { get; set; }

    public virtual ProfileInfo ProfileInfo { get; set; } = null!;

    public virtual IEnumerable<FileAchievement>? FilesAchievement { get; set; }
}
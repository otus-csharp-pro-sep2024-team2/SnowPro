using ProfileService.Application.Contracts.FileAchievementContracts;

namespace ProfileService.Application.Contracts.AchievementContracts;

public class AchievementDto
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

    public IEnumerable<FileAchievementDto>? FilesAchievement { get; set; }
}

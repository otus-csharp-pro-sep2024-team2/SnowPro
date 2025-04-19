using ProfileService.Application.Contracts.FileAchievementContracts;

namespace ProfileService.Application.Contracts.AchievementContracts;

public class CreatingAchievementWithFilesDto
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Список файлов
    /// </summary>
    public IEnumerable<CreatingFileAchievementDto>? FilesAchievement { get; set; }
}

using ProfileService.API.Models.FileAchievementModels;

namespace ProfileService.API.Models.AchievementModels;

public class CreatingAchievementWithFilesModel
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
    public IEnumerable<CreatingFileAchievementModel>? FilesAchievement { get; set; }
}

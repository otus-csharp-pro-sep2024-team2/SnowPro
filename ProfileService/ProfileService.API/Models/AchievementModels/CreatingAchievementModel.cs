using ProfileService.API.Models.FileAchievementModels;

namespace ProfileService.API.Models.AchievementModels;

public class CreatingAchievementModel
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }
}

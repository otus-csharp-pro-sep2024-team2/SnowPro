namespace ProfileService.API.Models.AchievementModels;

public class UpdatingAchievementModel
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

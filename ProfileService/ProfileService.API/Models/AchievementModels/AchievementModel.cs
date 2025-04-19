using ProfileService.API.Models.FileAchievementModels;
using ProfileService.Domain.Entities;

namespace ProfileService.API.Models.AchievementModels;

public class AchievementModel
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

    public IEnumerable<FileAchievementModel>? FilesAchievement { get; set; }
}

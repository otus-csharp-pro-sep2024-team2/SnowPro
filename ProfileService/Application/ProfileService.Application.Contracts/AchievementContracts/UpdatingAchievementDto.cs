namespace ProfileService.Application.Contracts.AchievementContracts;

public class UpdatingAchievementDto
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

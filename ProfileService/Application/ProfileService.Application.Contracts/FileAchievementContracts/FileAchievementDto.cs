namespace ProfileService.Application.Contracts.FileAchievementContracts;

public class FileAchievementDto
{
    public int Id { get; set; }
    public Guid FileId { get; set; }
    public int? AchievementId { get; set; }
}

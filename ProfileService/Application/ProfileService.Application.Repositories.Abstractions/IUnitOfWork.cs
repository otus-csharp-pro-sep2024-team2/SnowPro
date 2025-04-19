namespace ProfileService.Application.Repositories.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IAchievementRepository AchievementRepository { get; }
    IClientProfileInfoRepository ClientProfileInfoRepository { get; }
    IFileAchievementRepository FileAchievementRepository { get; }
    IInstructorProfileInfoRepository InstructorProfileInfoRepository { get; }
    ILevelTrainingRepository LevelTrainingRepository { get; }
    IPositionRepository PositionRepository { get; }
    IProfileInfoRepository ProfileInfoRepository { get; }
    ITypeSportEquipmentRepository TypeSportEquipmentRepository { get; }
    Task SaveChangesAsync();

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}

using Microsoft.EntityFrameworkCore.Storage;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Infrastructure.EntityFramework;

namespace ProfileService.Infrastructure.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private IAchievementRepository _achievementRepository;
    private IClientProfileInfoRepository _clientProfileInfoRepository;
    private IFileAchievementRepository _fileAchievementRepository;
    private IInstructorProfileInfoRepository _instructorProfileInfoRepository;
    private ILevelTrainingRepository _levelTrainingRepository;
    private IPositionRepository _positionRepository;
    private IProfileInfoRepository _profileInfoRepository;
    //ITypeSportEquipmentProfileRepository TypeSportEquipmentProfileRepository;
    private ITypeSportEquipmentRepository _typeSportEquipmentRepository;

    public IAchievementRepository AchievementRepository => _achievementRepository;

    public IClientProfileInfoRepository ClientProfileInfoRepository => _clientProfileInfoRepository;

    public IFileAchievementRepository FileAchievementRepository => _fileAchievementRepository;

    public IInstructorProfileInfoRepository InstructorProfileInfoRepository => _instructorProfileInfoRepository;

    public ILevelTrainingRepository LevelTrainingRepository => _levelTrainingRepository;

    public IPositionRepository PositionRepository => _positionRepository;

    public IProfileInfoRepository ProfileInfoRepository => _profileInfoRepository;

    public ITypeSportEquipmentRepository TypeSportEquipmentRepository => _typeSportEquipmentRepository;

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
        _achievementRepository = new AchievementRepository(context);
        _clientProfileInfoRepository = new ClientProfileInfoRepository(context);
        _fileAchievementRepository = new FileAchievementRepository(context);
        _instructorProfileInfoRepository = new InstructorProfileInfoRepository(context);
        _levelTrainingRepository = new LevelTrainingRepository(context);
        _positionRepository = new PositionRepository(context);
        _profileInfoRepository = new ProfileInfoRepository(context);
        //_typeSportEquipmentProfileRepository = new TypeSportEquipmentProfileRepository(context);
        _typeSportEquipmentRepository = new TypeSportEquipmentRepository(context);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    private IDbContextTransaction _transaction;
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync(); // Сохраняем изменения в DbContext
            await _transaction.CommitAsync(); // Фиксируем транзакцию
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
        _context.ChangeTracker.Clear(); // Сбрасываем отслеживаемые сущности
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}

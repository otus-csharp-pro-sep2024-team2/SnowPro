using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Repositories.Abstractions;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.EntityFramework;

namespace ProfileService.Infrastructure.Repositories.Implementations;

public class FileAchievementRepository : Repository<FileAchievement, int>, IFileAchievementRepository
{
    public FileAchievementRepository(DatabaseContext context) : base(context)
    {
    }

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id файла достижения. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns>Файл достижения</returns>
    public override async Task<FileAchievement?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<FileAchievement>().SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    /// <summary>
    /// Получить список файлов достижения.
    /// </summary>
    /// <param name="id"> Id достижения. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Список файлов достижения. </returns>
    public async Task<List<FileAchievement>> GetByAchievementIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<FileAchievement>()
            .Where(l => l.AchievementId == id)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Получить список файлов достижений.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список файлов достижений. </returns>
    public async Task<List<FileAchievement>> GetPagedAsync(int page, int itemsPerPage)
    {
        return await GetAll()
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }
}

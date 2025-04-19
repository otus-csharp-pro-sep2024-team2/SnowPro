using ProfileService.Domain.Entities;

namespace ProfileService.Application.Repositories.Abstractions;

public interface IFileAchievementRepository : IRepository<FileAchievement, int>
{
    /// <summary>
    /// Получить список файлов достижения.
    /// </summary>
    /// <param name="id"> Id достижения. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Список файлов достижения. </returns>
    Task<List<FileAchievement>> GetByAchievementIdAsync(int id, CancellationToken cancellationToken);
}

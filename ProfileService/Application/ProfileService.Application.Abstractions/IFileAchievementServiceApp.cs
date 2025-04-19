using ProfileService.Application.Contracts.FileAchievementContracts;

namespace ProfileService.Application.Abstractions;

public interface IFileAchievementServiceApp
{
    /// <summary>
    /// Получить файл достижения.
    /// </summary>
    /// <param name="id"> Идентификатор файла достижения. </param>
    /// <returns> ДТО файла достижения. </returns>
    Task<IEnumerable<FileAchievementDto>> GetByAchievementIdAsync(int id);

    /// <summary>
    /// Создать файл достижения.
    /// </summary>
    /// <param name="achievementId"> id достижения. </param>
    /// <param name="creatingFileAchievementDto"> ДТО создаваемого файла достижения. </param>
    /// <returns>id файла достижения</returns>
    Task<int> CreateAsync(int achievementId, CreatingFileAchievementDto creatingFileAchievementDto);

    /// <summary>
    /// Удалить файл достижения.
    /// </summary>
    /// <param name="id"> Идентификатор файла достижения. </param>
    /// <returns></returns>
    Task DeleteAsync(int id);
}

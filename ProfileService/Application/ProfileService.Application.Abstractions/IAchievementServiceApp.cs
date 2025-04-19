using ProfileService.Application.Contracts.AchievementContracts;

namespace ProfileService.Application.Abstractions;

public interface IAchievementServiceApp
{
    /// <summary>
    /// Получить достижение со списком файлов.
    /// </summary>
    /// <param name="id"> Идентификатор достижения. </param>
    /// <returns> ДТО достижения со списком файлов. </returns>
    Task<AchievementDto> GetByIdAsync(int id);

    /// <summary>
    /// Получить достижения пользователя со списком файлов.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> Список ДТО достижений со списком файлов. </returns>
    Task<IEnumerable<AchievementDto>> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Создать достижение пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="creatingAchievementDto"> ДТО создаваемого достижения. </param>
    /// <returns> Идентификатор достижения. </returns>
    Task<int> CreateAsync(Guid userId, CreatingAchievementDto creatingAchievementDto);

    /// <summary>
    /// Создать достижение со списком файлов.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="creatingAchievementWithFilesDto"> ДТО создаваемого достижения. </param>
    /// <returns> Идентификатор достижения. </returns>
    Task<int> CreateWithFilesAsync(Guid userId, CreatingAchievementWithFilesDto creatingAchievementWithFilesDto);

    /// <summary>
    /// Изменить достижение.
    /// </summary>
    /// <param name="id"> Идентификатор достижения. </param>
    /// <param name="updatingAchievementDto"> ДТО редактируемого достижения. </param>
    Task UpdateAsync(int id, UpdatingAchievementDto updatingAchievementDto);


    /// <summary>
    /// Удалить достижение со списком файлов.
    /// </summary>
    /// <param name="id"> Идентификатор достижения. </param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Получить постраничный список.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    Task<ICollection<AchievementDto>> GetPagedAsync(int page, int itemsPerPage);
}

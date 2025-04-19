using ProfileService.Application.Contracts.ProfileInfoContracts;

namespace ProfileService.Application.Abstractions;

public interface IProfileInfoServiceApp
{
    /// <summary>
    /// Получить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор. </param>
    /// <returns> ДТО профиля. </returns>
    Task<ProfileInfoDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить профиль пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> ДТО профиля. </returns>
    Task<ProfileInfoDto> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Создать профиль.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="creatingProfileDto"> ДТО создаваемого профиля. </param>
    Task<Guid> CreateAsync(Guid userId, CreatingProfileInfoDto creatingProfileDto);

    /// <summary>
    /// Изменить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор профиля. </param>
    /// <param name="updatingProfileDto"> ДТО редактируемого профиля. </param>
    Task UpdateAsync(Guid id, UpdatingProfileInfoDto updatingProfileDto);

    /// <summary>
    /// Удалить профиль.
    /// </summary>
    /// <param name="id"> Идентификатор профиля. </param>
    Task<ProfileInfoDto> DeleteAsync(Guid id);

    /// <summary>
    /// Получить постраничный список профилей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    Task<ICollection<ProfileInfoDto>> GetPagedAsync(int page, int itemsPerPage);
}

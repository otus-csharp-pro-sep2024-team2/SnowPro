using ProfileService.Application.Contracts.ClientProfileInfoContracts;

namespace ProfileService.Application.Abstractions;

public interface IClientProfileInfoServiceApp
{
    /// <summary>
    /// Получить профиль клиента.
    /// </summary>
    /// <param name="id"> Идентификатор профиля клиента. </param>
    /// <returns> ДТО профиля клиента. </returns>
    Task<ClientProfileInfoDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить профиль клиента по id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> ДТО профиля клиента. </returns>
    Task<ClientProfileInfoDto> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Создать профиль.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="creatingProfileDto"> ДТО создаваемого профиля. </param>
    Task<Guid> CreateAsync(Guid userId, CreatingClientProfileInfoDto creatingProfileDto);


    /// <summary>
    /// Изменить профиль клиента.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="updatingProfileDto"> ДТО редактируемого профиля клиента. </param>
    Task UpdateAsync(Guid userId, UpdatingClientProfileInfoDto updatingProfileDto);

    /// <summary>
    /// Удалить профиль клиента.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    Task<ClientProfileInfoDto> DeleteAsync(Guid userId);

    /// <summary>
    /// Получить список профилей клиентов.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Страница профилей клиентов. </returns>
    Task<IReadOnlyList<ClientProfileInfoDto>> GetPagedAsync(int page, int itemsPerPage);
}

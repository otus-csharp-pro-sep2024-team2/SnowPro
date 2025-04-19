using ProfileService.Application.Contracts.InstructorProfileInfoContracts;
using ProfileService.Common.Enums;

namespace ProfileService.Application.Abstractions;

public interface IInstructorProfileInfoServiceApp
{
    /// <summary>
    /// Получить профиль инструктора.
    /// </summary>
    /// <param name="id"> Идентификатор профиля инструктора. </param>
    /// <returns> ДТО профиля инструктора. </returns>
    Task<InstructorProfileInfoDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Получить профиль инструктора по id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> ДТО профиля инструктора. </returns>
    Task<InstructorProfileInfoDto> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Создать профиль инструктора.
    /// </summary>
    /// <param name="userId"> Id пользователя. </param>
    /// <param name="creatingInstructorProfile"> Профиль инструктора. </param>
    Task<Guid> CreateAsync(Guid userId, CreatingInstructorProfileInfoDto creatingInstructorProfile);

    /// <summary>
    /// Изменить профиль инструктора по id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="updatingInstructorProfileDto"> ДТО редактируемого профиля инструктора. </param>
    /// <returns></returns>
    Task UpdateAsync(Guid userId, UpdatingInstructorProfileInfoDto updatingInstructorProfileDto);

    /// <summary>
    /// Подтверждение изменений профиля
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="profileStatus"> Статус профиля. </param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    Task<InstructorProfileInfoDto> ConfirmСhangesAsync(Guid userId, ProfileStatuses profileStatus);

    /// <summary>
    /// Удалить профиль инструктора по id пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns></returns>
    Task<InstructorProfileInfoDto> DeleteAsync(Guid userId);

    /// <summary>
    /// Получить постраничный список профилей инструктора.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Страница профилей инструктора. </returns>
    Task<ICollection<InstructorProfileInfoDto>> GetPagedAsync(int page, int itemsPerPage);

    /// <summary>
    /// Получить cписок профилей инструктора требующих подтверждение изменений
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    Task<ICollection<InstructorProfileInfoDto>> GetRequiredConfirmationAsync(int page, int itemsPerPage);
}

using ProfileService.Common.Enums;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Repositories.Abstractions;

public interface IInstructorProfileInfoRepository : IRepository<InstructorProfileInfo, Guid>
{
    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Профиль. </returns>
    Task<InstructorProfileInfo> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить профиль инструктора по Id пользователя и статусу профиля.
    /// </summary>
    /// <param name="userId"> Id пользователя. </param>
    /// <param name="profileStatus"> Статус профиля. </param>
    /// <returns> Профиль инструктора. </returns>
    Task<InstructorProfileInfo?> GetByUserIdAndStatusAsync(Guid userId, ProfileStatuses profileStatus);

    /// <summary>
    /// Получить список профилей инструктора.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список профилей инструктора. </returns>
    Task<List<InstructorProfileInfo>> GetPagedAsync(int page, int itemsPerPage);

    /// <summary>
    /// Получить cписок профилей инструктора требующих подтверждение изменений
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns></returns>
    Task<List<InstructorProfileInfo>> GetRequiredConfirmationPagedAsync(int page, int itemsPerPage);

}
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Repositories.Abstractions;

public interface IClientProfileInfoRepository : IRepository<ClientProfileInfo, Guid>
{
    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Профиль клиента. </returns>
    Task<ClientProfileInfo?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить список профилей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список профилей. </returns>
    Task<IReadOnlyList<ClientProfileInfo>> GetPagedAsync(int page, int itemsPerPage);
}
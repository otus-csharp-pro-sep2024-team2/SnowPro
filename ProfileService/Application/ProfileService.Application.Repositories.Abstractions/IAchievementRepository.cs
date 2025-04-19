using ProfileService.Domain.Entities;

namespace ProfileService.Application.Repositories.Abstractions;

public interface IAchievementRepository : IRepository<Achievement, int>
{
    /// <summary>
    /// Получить список достижений по id профиля
    /// </summary>
    /// <param name="profileInfoId"> Id профиля. </param>
    /// <returns> Список достижений пользователя. </returns>
    Task<IEnumerable<Achievement>> GetByProfileInfoIdAsync(Guid profileInfoId);

    /// <summary>
    /// Получить список достижений.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список достижений. </returns>
    Task<List<Achievement>> GetPagedAsync(int page, int itemsPerPage);
}
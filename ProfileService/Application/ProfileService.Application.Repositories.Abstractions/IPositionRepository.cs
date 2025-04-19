using ProfileService.Domain.Entities;

namespace ProfileService.Application.Repositories.Abstractions;

public interface IPositionRepository : IRepository<Position, int>
{
    /// <summary>
    /// Получить список должностей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список должностей. </returns>
    Task<List<Position>> GetPagedAsync(int page, int itemsPerPage);
}
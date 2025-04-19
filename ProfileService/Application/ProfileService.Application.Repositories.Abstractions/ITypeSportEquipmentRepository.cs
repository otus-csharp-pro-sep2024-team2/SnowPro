using ProfileService.Domain.Entities;

namespace ProfileService.Application.Repositories.Abstractions;

public interface ITypeSportEquipmentRepository : IRepository<TypeSportEquipment, int>
{
    /// <summary>
    /// Получить сущность по Name.
    /// </summary>
    /// <param name="name"> Название сущности. </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Тип спортивного оборудования. </returns>
    Task<TypeSportEquipment> GetByNameAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Получить список типов спортивного оборудования.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Список типов спортивного оборудования. </returns>
    Task<List<TypeSportEquipment>> GetPagedAsync(int page, int itemsPerPage);
}
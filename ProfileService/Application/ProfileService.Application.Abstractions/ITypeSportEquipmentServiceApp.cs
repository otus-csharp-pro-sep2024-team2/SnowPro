using ProfileService.Application.Contracts.TypeSportEquipmentContracts;

namespace ProfileService.Application.Abstractions;

public interface ITypeSportEquipmentServiceApp
{
    /// <summary>
    /// Получить тип спортивного оборудования.
    /// </summary>
    /// <param name="id"> Идентификатор типа спортивного оборудования. </param>
    /// <returns> ДТО типа спортивного оборудования. </returns>
    Task<TypeSportEquipmentDto> GetByIdAsync(int id);

    /// <summary>
    /// Создать тип спортивного оборудования.
    /// </summary>
    /// <param name="creatingTypeSportEquipmentDto"> ДТО создаваемого типа спортивного оборудования. </param>
    Task<int> CreateAsync(CreatingTypeSportEquipmentDto creatingTypeSportEquipmentDto);

    /// <summary>
    /// Изменить тип спортивного оборудования.
    /// </summary>
    /// <param name="id"> Идентификатор типа спортивного оборудования. </param>
    /// <param name="updatingTypeSportEquipmentDto"> ДТО редактируемого типа спортивного оборудования. </param>
    Task UpdateAsync(int id, UpdatingTypeSportEquipmentDto updatingTypeSportEquipmentDto);

    /// <summary>
    /// Удалить тип спортивного оборудования.
    /// </summary>
    /// <param name="id"> Идентификатор типа спортивного оборудования. </param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Получить постраничный список.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Страница. </returns>
    Task<ICollection<TypeSportEquipmentDto>> GetPagedAsync(int page, int itemsPerPage);
}

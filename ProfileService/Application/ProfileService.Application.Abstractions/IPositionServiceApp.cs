using ProfileService.Application.Contracts.PositionContracts;

namespace ProfileService.Application.Abstractions;

public interface IPositionServiceApp
{
    /// <summary>
    /// Получить должность.
    /// </summary>
    /// <param name="id"> Идентификатор должности. </param>
    /// <returns> ДТО должности. </returns>
    Task<PositionDto> GetByIdAsync(int id);

    /// <summary>
    /// Создать должность.
    /// </summary>
    /// <param name="creatingPositionDto"> ДТО создаваемого должности. </param>
    Task<int> CreateAsync(CreatingPositionDto creatingPositionDto);

    /// <summary>
    /// Изменить должность.
    /// </summary>
    /// <param name="id"> Идентификатор должности. </param>
    /// <param name="updatingPositionDto"> ДТО редактируемого должности. </param>
    Task UpdateAsync(int id, UpdatingPositionDto updatingPositionDto);

    /// <summary>
    /// Удалить должность.
    /// </summary>
    /// <param name="id"> Идентификатор должности. </param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Получить постраничный список.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="itemsPerPage"> Количество элементов на странице. </param>
    /// <returns> Страница. </returns>
    Task<ICollection<PositionDto>> GetPagedAsync(int page, int itemsPerPage);
}

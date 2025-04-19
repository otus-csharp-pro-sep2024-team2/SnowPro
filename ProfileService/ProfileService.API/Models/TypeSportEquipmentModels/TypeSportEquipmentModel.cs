using ProfileService.Domain.Entities;

namespace ProfileService.API.Models.TypeSportEquipmentModels;
/// <summary>
/// Модель типа спортивного оборудования.
/// </summary>
public class TypeSportEquipmentModel
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;
}
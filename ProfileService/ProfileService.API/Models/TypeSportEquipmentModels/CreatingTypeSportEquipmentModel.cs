using ProfileService.Domain.Entities;

namespace ProfileService.API.Models.TypeSportEquipmentModels;
/// <summary>
/// Модель создаваемого типа спортивного оборудования.
/// </summary>
public class CreatingTypeSportEquipmentModel
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;
}

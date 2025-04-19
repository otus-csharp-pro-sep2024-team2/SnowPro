using ProfileService.Domain.Entities;

namespace ProfileService.Application.Contracts.TypeSportEquipmentContracts;
/// <summary>
/// ДТО создаваемого типа спортивного оборудования.
/// </summary>
public class CreatingTypeSportEquipmentDto
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;
}

namespace ProfileService.Application.Contracts.TypeSportEquipmentContracts;
/// <summary>
/// ДТО редактируемого типа спортивного оборудования.
/// </summary>
public class UpdatingTypeSportEquipmentDto
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;
}
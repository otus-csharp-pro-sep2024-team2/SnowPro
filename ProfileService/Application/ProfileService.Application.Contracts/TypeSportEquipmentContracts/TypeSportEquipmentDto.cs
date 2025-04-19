namespace ProfileService.Application.Contracts.TypeSportEquipmentContracts;
/// <summary>
/// ДТО типа спортивного оборудования.
/// </summary>
public class TypeSportEquipmentDto
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
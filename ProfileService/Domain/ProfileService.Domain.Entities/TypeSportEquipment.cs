using System.Text.Json.Serialization;

namespace ProfileService.Domain.Entities;

/// <summary>
/// Тип спортивного оборудования.
/// </summary>
public class TypeSportEquipment: IEntity<int>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual IEnumerable<ProfileInfo>? ProfileInfo { get; set; }

    [JsonIgnore]
    public virtual IEnumerable<TypeSportEquipmentProfile>? TypeSportEquipmentProfile { get; set; }
}
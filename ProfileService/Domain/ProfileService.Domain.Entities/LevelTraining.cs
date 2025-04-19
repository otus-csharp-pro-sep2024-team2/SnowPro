using System.Text.Json.Serialization;

namespace ProfileService.Domain.Entities;

/// <summary>
/// Уровень подготовки.
/// </summary>
public class LevelTraining: IEntity<int>
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
    public virtual IEnumerable<TypeSportEquipmentProfile>? TypeSportEquipmentProfile { get; set; }
}
using System.Text.Json.Serialization;

namespace ProfileService.Domain.Entities;

public class TypeSportEquipmentProfile: IEntity<int>
{
    public int Id { get; set; }

    public Guid ProfileId { get; set; }

    [JsonIgnore]
    public virtual ProfileInfo ProfileInfo { get; set; } = null!;

    public int? TypeSportEquipmentId { get; set; }

    [JsonIgnore]
    public virtual TypeSportEquipment? TypeSportEquipment { get; set; }

    public int? LevelTrainingId { get; set; }

    [JsonIgnore]
    public virtual LevelTraining? LevelTraining { get; set; }
}
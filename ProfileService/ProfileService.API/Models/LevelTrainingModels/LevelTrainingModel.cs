namespace ProfileService.API.Models.LevelTrainingModels;
/// <summary>
/// Модель уровня подготовки.
/// </summary>
public class LevelTrainingModel
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
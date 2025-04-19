namespace ProfileService.Application.Contracts.LevelTrainingContracts;
/// <summary>
/// ДТО уровня подготовки.
/// </summary>
public class LevelTrainingDto
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

namespace ProfileService.Application.Contracts.LevelTrainingContracts;
/// <summary>
/// ДТО редактируемого уровня подготовки.
/// </summary>
public class UpdatingLevelTrainingDto
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Name { get; set; } = null!;
}

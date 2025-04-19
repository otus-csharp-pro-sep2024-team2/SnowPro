namespace ProfileService.Application.Contracts.PositionContracts;
/// <summary>
/// ДТО редактируемой должности.
/// </summary>
public class UpdatingPositionDto
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; } = null!;
}

namespace ProfileService.Application.Contracts.PositionContracts;
/// <summary>
/// ДТО создаваемой должности.
/// </summary>
public class CreatingPositionDto
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; } = null!;
}

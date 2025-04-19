namespace ProfileService.Application.Contracts.PositionContracts;
/// <summary>
/// ДТО должности.
/// </summary>
public class PositionDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; } = null!;
}
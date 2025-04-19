namespace ProfileService.Domain.Entities;
/// <summary>
/// Должность.
/// </summary>
public class Position: IEntity<int>
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
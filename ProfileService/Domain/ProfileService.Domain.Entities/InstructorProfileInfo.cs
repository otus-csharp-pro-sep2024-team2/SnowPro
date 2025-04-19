namespace ProfileService.Domain.Entities;
/// <summary>
/// Профиль инструктора.
/// </summary>
public class InstructorProfileInfo: ProfileInfo
{
    /// <summary>
    /// Должность.
    /// </summary>
    public int? PositionId { get; set; }

    public virtual Position? Position { get; set; }

    /// <summary>
    /// Дата принятия на работу.
    /// </summary>
    public DateTime? HireDate { get; set; }

    /// <summary>
    /// Дата увольнения.
    /// </summary>
    public DateTime? DateDismissal { get; set; }

    /// <summary>
    /// Стаж до принятия на работу, лет.
    /// </summary>
    public int? ExperienceBeforeHiring { get; set; } = default;
}
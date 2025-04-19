using ProfileService.Application.Contracts.ProfileInfoContracts;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Contracts.InstructorProfileInfoContracts;
/// <summary>
/// ДТО профиля .
/// </summary>
public class InstructorProfileInfoDto : ProfileInfoDto
{
    /// <summary>
    /// Должность.
    /// </summary>
    public int? PositionId { get; set; }

    public Position? Position { get; set; }

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
    public int? ExperienceBeforeHiring { get; set; }
}
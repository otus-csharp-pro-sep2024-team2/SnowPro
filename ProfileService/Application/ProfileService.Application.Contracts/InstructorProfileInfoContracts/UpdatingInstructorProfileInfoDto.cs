using ProfileService.Application.Contracts.ProfileInfoContracts;

namespace ProfileService.Application.Contracts.InstructorProfileInfoContracts;
/// <summary>
/// ДТО редактируемого профиля .
/// </summary>
public class UpdatingInstructorProfileInfoDto : UpdatingProfileInfoDto
{
    /// <summary>
    /// Должность.
    /// </summary>
    public int? PositionId { get; set; }

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
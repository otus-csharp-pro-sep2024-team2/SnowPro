using ProfileService.API.Models.ProfileInfoModels;

namespace ProfileService.API.Models.InstructorProfileInfoModels;
/// <summary>
/// Модель создаваемого профиля инструктора.
/// </summary>
public class CreatingInstructorProfileInfoModel : CreatingProfileInfoModel
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
    public int? ExperienceBeforeHiring { get; set; } = default;
}
using ProfileService.Application.Contracts.TypeSportEquipmentProfileInfoContracts;
using ProfileService.Common.Enums;

namespace ProfileService.Application.Contracts.ProfileInfoContracts;
/// <summary>
/// ДТО профиля.
/// </summary>
public class ProfileInfoDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Дата обновления.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Статус.
    /// </summary>
    public ProfileStatuses Status { get; set; }

    /// <summary>
    /// Активность профиля.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Профиль удален.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid? UpdatedUserId { get; set; }

    /// <summary>
    /// Id фото профиля.
    /// </summary>
    public Guid? PhotoId { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? Surname { get; set; }

    /// Имя.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Пол.
    /// </summary>
    public Gender? Gender { get; set; }

    /// <summary>
    /// Телефон.
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Телеграм.
    /// </summary>
    public string? TelegramName { get; set; }

    /// <summary>
    /// Почта.
    /// </summary>
    public string Email { get; set; } = null!;

    public virtual IEnumerable<TypeSportEquipmentProfileInfoDto>? TypeSportEquipmentProfile { get; set; }
}
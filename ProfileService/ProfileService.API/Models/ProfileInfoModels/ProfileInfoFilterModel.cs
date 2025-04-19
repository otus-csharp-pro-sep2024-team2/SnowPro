using ProfileService.Common.Enums;

namespace ProfileService.API.Models.ProfileInfoModels;

public class ProfileInfoFilterModel
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
    /// Фамилия.
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
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
}
using ProfileService.Application.Contracts.ProfileInfoContracts;

namespace ProfileService.Application.Contracts.ClientProfileInfoContracts;
/// <summary>
/// ДТО создаваемого профиля пользователя.
/// </summary>
public class CreatingClientProfileInfoDto : CreatingProfileInfoDto
{
    public Guid? OwnerProfileInfoId { get; set; }
}
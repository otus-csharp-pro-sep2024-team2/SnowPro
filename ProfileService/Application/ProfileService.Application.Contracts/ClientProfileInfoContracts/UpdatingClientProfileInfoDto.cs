using ProfileService.Application.Contracts.ProfileInfoContracts;

namespace ProfileService.Application.Contracts.ClientProfileInfoContracts;
/// <summary>
/// ДТО редактируемого профиля пользователя.
/// </summary>
public class UpdatingClientProfileInfoDto : UpdatingProfileInfoDto
{
    public Guid? OwnerProfileInfoId { get; set; }
}
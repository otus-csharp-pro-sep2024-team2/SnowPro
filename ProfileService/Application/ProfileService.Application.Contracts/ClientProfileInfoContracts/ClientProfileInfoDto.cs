using ProfileService.Application.Contracts.ProfileInfoContracts;

namespace ProfileService.Application.Contracts.ClientProfileInfoContracts;
/// <summary>
/// ДТО профиля пользователя.
/// </summary>
public class ClientProfileInfoDto : ProfileInfoDto
{
    public Guid? OwnerProfileInfoId { get; set; }
}
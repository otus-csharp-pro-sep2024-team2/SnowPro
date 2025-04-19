using ProfileService.API.Models.ProfileInfoModels;

namespace ProfileService.API.Models.ClientProfileInfoModels;

public class CreatingClientProfileInfoModel : CreatingProfileInfoModel
{
    public Guid? OwnerProfileInfoId { get; set; }
}
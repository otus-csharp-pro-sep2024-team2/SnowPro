using ProfileService.API.Models.ProfileInfoModels;

namespace ProfileService.API.Models.ClientProfileInfoModels;

public class UpdatingClientProfileInfoModel : UpdatingProfileInfoModel
{
    public Guid? OwnerProfileInfoId { get; set; }
}
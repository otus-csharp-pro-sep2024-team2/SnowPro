using ProfileService.API.Models.ProfileInfoModels;

namespace ProfileService.API.Models.ClientProfileInfoModels;

public class ClientProfileInfoModel : ProfileInfoModel
{
    public Guid? OwnerProfileInfoId { get; set; }
}

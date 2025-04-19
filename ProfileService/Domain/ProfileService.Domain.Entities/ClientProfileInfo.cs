using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileService.Domain.Entities;
/// <summary>
/// Профиль клиента.
/// </summary>
public class ClientProfileInfo : ProfileInfo
{
    public Guid? OwnerProfileInfoId { get; set; }

    [ForeignKey("OwnerProfileInfoId")]
    public virtual ProfileInfo? ProfileInfo { get; set; }
}
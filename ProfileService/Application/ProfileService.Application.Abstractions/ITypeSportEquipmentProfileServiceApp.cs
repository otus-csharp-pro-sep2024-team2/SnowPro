using ProfileService.Application.Contracts.TypeSportEquipmentProfileInfoContracts;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Abstractions;

public interface ITypeSportEquipmentProfileServiceApp
{
    /// <summary>
    /// Создать спортивное оборудование.
    /// </summary>
    /// <param name="creatingSportEquipmentDto"> ДТО создаваемого спортивного оборудования. </param>
    Task<TypeSportEquipmentProfile> CreateAsync(CreatingTypeSportEquipmentProfileInfoDto creatingSportEquipmentDto);
}

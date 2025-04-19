using AutoMapper;
using ProfileService.Application.Contracts.TypeSportEquipmentContracts;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services.Mapping;
/// <summary>
/// Профиль автомаппера для сущности типа спортивного оборудования.
/// </summary>
public class TypeSportEquipmentMappingsProfile: Profile
{
    public TypeSportEquipmentMappingsProfile()
    {
        CreateMap<TypeSportEquipment, TypeSportEquipmentDto>();
        CreateMap<TypeSportEquipmentDto, TypeSportEquipment>()
            .ForMember(t => t.ProfileInfo, map => map.Ignore())
            .ForMember(t => t.TypeSportEquipmentProfile, map => map.Ignore())
            ;
        CreateMap<CreatingTypeSportEquipmentDto, TypeSportEquipment>()
            .ForMember(t => t.Id, map => map.Ignore())
            .ForMember(t => t.ProfileInfo, map => map.Ignore())
            .ForMember(t => t.TypeSportEquipmentProfile, map => map.Ignore())
            ;
        CreateMap<UpdatingTypeSportEquipmentDto, TypeSportEquipment>()
            .ForMember(t => t.Id, map => map.Ignore())
            .ForMember(t => t.ProfileInfo, map => map.Ignore())
            .ForMember(t => t.TypeSportEquipmentProfile, map => map.Ignore());
    }
}
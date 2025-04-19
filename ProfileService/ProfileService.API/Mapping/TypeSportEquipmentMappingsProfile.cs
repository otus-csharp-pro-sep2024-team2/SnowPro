using AutoMapper;
using ProfileService.API.Models.TypeSportEquipmentModels;
using ProfileService.Application.Contracts.TypeSportEquipmentContracts;

namespace ProfileService.API.Mapping;

public class TypeSportEquipmentMappingsProfile: Profile
{
    public TypeSportEquipmentMappingsProfile()
    {
        CreateMap<TypeSportEquipmentModel, TypeSportEquipmentDto>();
        CreateMap<TypeSportEquipmentDto, TypeSportEquipmentModel>();
        CreateMap<CreatingTypeSportEquipmentModel, CreatingTypeSportEquipmentDto>();
        CreateMap<UpdatingTypeSportEquipmentModel, UpdatingTypeSportEquipmentDto>();
    }
}

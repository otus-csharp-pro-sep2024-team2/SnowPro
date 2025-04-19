using AutoMapper;
using ProfileService.API.Models.TypeSportEquipmentProfileInfoModels;
using ProfileService.Application.Contracts.TypeSportEquipmentProfileInfoContracts;

namespace ProfileService.API.Mapping;

public class TypeSportEquipmentProfileInfoMappingsProfile : Profile
{
    public TypeSportEquipmentProfileInfoMappingsProfile()
    {
        CreateMap<TypeSportEquipmentProfileInfoModel, TypeSportEquipmentProfileInfoDto>();
        CreateMap<TypeSportEquipmentProfileInfoDto, TypeSportEquipmentProfileInfoModel>();
        CreateMap<CreatingTypeSportEquipmentProfileInfoModel, CreatingTypeSportEquipmentProfileInfoDto>();
    }
}
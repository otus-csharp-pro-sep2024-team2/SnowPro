using AutoMapper;
using ProfileService.Application.Contracts.TypeSportEquipmentProfileInfoContracts;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services.Mapping;

public class TypeSportEquipmentProfileInfoMappingsProfile : Profile
{
    public TypeSportEquipmentProfileInfoMappingsProfile()
    {
        CreateMap<TypeSportEquipmentProfile, TypeSportEquipmentProfileInfoDto>()
            .ForMember(t => t.TypeSportEquipmentName, map => map.MapFrom(t => t.TypeSportEquipment!.Name))
            .ForMember(t => t.LevelTrainingName, map => map.MapFrom(t => t.LevelTraining!.Name))
        ;

        CreateMap<TypeSportEquipmentProfileInfoDto, TypeSportEquipmentProfile>()
            .ForPath(t => t.TypeSportEquipment!.Name, map => map.MapFrom(t => t.TypeSportEquipmentName))
            .ForPath(t => t.LevelTraining!.Name, map => map.MapFrom(t => t.LevelTrainingName))
            .ForMember(t => t.Id, map => map.Ignore())
            .ForMember(t => t.ProfileId, map => map.Ignore())
            .ForMember(t => t.ProfileInfo, map => map.Ignore())
            .ForMember(t => t.TypeSportEquipmentId, map => map.Ignore())
            .ForMember(t => t.TypeSportEquipment, map => map.Ignore())
            .ForMember(t => t.LevelTrainingId, map => map.Ignore())
            .ForMember(t => t.LevelTraining, map => map.Ignore())
        ;

        CreateMap<CreatingTypeSportEquipmentProfileInfoDto, TypeSportEquipmentProfile>()
            .ForPath(t => t.TypeSportEquipment!.Name, map => map.MapFrom(t => t.TypeSportEquipmentName))
            .ForPath(t => t.LevelTraining!.Name, map => map.MapFrom(t => t.LevelTrainingName))
            .ForMember(t => t.Id, map => map.Ignore())
            .ForMember(t => t.ProfileId, map => map.Ignore())
            .ForMember(t => t.ProfileInfo, map => map.Ignore())
            .ForMember(t => t.TypeSportEquipmentId, map => map.Ignore())
            .ForMember(t => t.TypeSportEquipment, map => map.Ignore())
            .ForMember(t => t.LevelTrainingId, map => map.Ignore())
            .ForMember(t => t.LevelTraining, map => map.Ignore())
       ;

       CreateMap<TypeSportEquipmentProfile, CreatingTypeSportEquipmentProfileInfoDto>()
            .ForMember(t => t.TypeSportEquipmentName, map => map.MapFrom(t => t.TypeSportEquipment!.Name))
            .ForMember(t => t.LevelTrainingName, map => map.MapFrom(t => t.LevelTraining!.Name))
       ;
    }
}
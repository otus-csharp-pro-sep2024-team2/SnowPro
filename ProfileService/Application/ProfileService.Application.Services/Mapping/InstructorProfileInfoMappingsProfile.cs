using AutoMapper;
using ProfileService.Application.Contracts.InstructorProfileInfoContracts;
using ProfileService.Domain.Entities;
using SnowPro.Shared.Contracts;

namespace ProfileService.Application.Services.Mapping;

public class InstructorProfileInfoMappingsProfile : Profile
{
    public InstructorProfileInfoMappingsProfile()
    {
        CreateMap<InstructorProfileInfo, InstructorProfileInfoDto>()
            .ForMember(p => p.TypeSportEquipmentProfile,
                map => map.MapFrom(p => p.TypeSportEquipmentProfile))
        ;

        CreateMap<InstructorProfileInfoDto, InstructorProfileInfo>()
            .ForMember(p => p.Achievements, map => map.Ignore())
            .ForMember(p => p.OwnerProfileInfo, map => map.Ignore())
            .ForMember(p => p.TypeSportEquipment, map => map.Ignore())
            .ForMember(p => p.VersionNumber, map => map.Ignore())
            .ForMember(p => p.IsCurrentVersion, map => map.Ignore())
            .ForMember(p => p.ProfileType, map => map.Ignore())
            .ForMember(p => p.TypeSportEquipmentProfile, 
                map => map.MapFrom(p => p.TypeSportEquipmentProfile))
        ;

        CreateMap<CreatingInstructorProfileInfoDto, InstructorProfileInfo>()
            .ForMember(p => p.Id, map => map.Ignore())
            .ForMember(p => p.UserId, map => map.Ignore())
            .ForMember(p => p.CreatedDate, map => map.Ignore())
            .ForMember(p => p.UpdatedDate, map => map.Ignore())
            .ForMember(p => p.Status, map => map.Ignore())
            .ForMember(p => p.IsActive, map => map.Ignore())
            .ForMember(p => p.IsDeleted, map => map.Ignore())
            .ForMember(p => p.UpdatedUserId, map => map.Ignore())
            .ForMember(p => p.Achievements, map => map.Ignore())
            .ForMember(p => p.OwnerProfileInfo, map => map.Ignore())
            .ForMember(p => p.TypeSportEquipment, map => map.Ignore())
            .ForMember(p => p.Position, map => map.Ignore())
            .ForMember(p => p.VersionNumber, map => map.Ignore())
            .ForMember(p => p.IsCurrentVersion, map => map.Ignore())
            .ForMember(p => p.ProfileType, map => map.Ignore())
            .ForMember(p => p.TypeSportEquipmentProfile, 
                map => map.MapFrom(p => p.TypeSportEquipmentProfile))
        ;

        CreateMap<InstructorProfileInfo, CreatingInstructorProfileInfoDto>()
            .ForMember(p => p.TypeSportEquipmentProfile,
                map => map.MapFrom(p => p.TypeSportEquipmentProfile))
        ;

        CreateMap<UpdatingInstructorProfileInfoDto, InstructorProfileInfo>()
            .ForMember(p => p.Id, map => map.Ignore())
            .ForMember(p => p.UserId, map => map.Ignore())
            .ForMember(p => p.CreatedDate, map => map.Ignore())
            .ForMember(p => p.Achievements, map => map.Ignore())
            .ForMember(p => p.OwnerProfileInfo, map => map.Ignore())
            .ForMember(p => p.TypeSportEquipment, map => map.Ignore())
            .ForMember(p => p.Position, map => map.Ignore())
            .ForMember(p => p.VersionNumber, map => map.Ignore())
            .ForMember(p => p.IsCurrentVersion, map => map.Ignore())
            .ForMember(p => p.ProfileType, map => map.Ignore())
            .ForMember(p => p.TypeSportEquipmentProfile,
                map => map.MapFrom(p => p.TypeSportEquipmentProfile))
        ;

        CreateMap<InstructorProfileInfoDto, SharedProfileInfoDto>();
    }
}
using AutoMapper;
using ProfileService.Application.Contracts.AchievementContracts;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services.Mapping;

public class AchievementMappingsProfile : Profile
{
    public AchievementMappingsProfile()
    {
        CreateMap<Achievement, AchievementDto>();

        CreateMap<AchievementDto, Achievement>()
            .ForMember(a => a.ProfileInfo, map => map.Ignore())
            .ForMember(a => a.FilesAchievement, map => map.Ignore());

        CreateMap<CreatingAchievementWithFilesDto, Achievement>()
            .ForMember(a => a.Id, map => map.Ignore())
            .ForMember(a => a.ProfileInfoId, map => map.Ignore())
            .ForMember(a => a.ProfileInfo, map => map.Ignore())
            .ForMember(a => a.FilesAchievement, map => map.Ignore());

        CreateMap<CreatingAchievementDto, Achievement>()
            .ForMember(a => a.Id, map => map.Ignore())
            .ForMember(a => a.ProfileInfoId, map => map.Ignore())
            .ForMember(a => a.ProfileInfo, map => map.Ignore())
            .ForMember(a => a.FilesAchievement, map => map.Ignore());

        CreateMap<UpdatingAchievementDto, Achievement>()
            .ForMember(a => a.Id, map => map.Ignore())
            .ForMember(a => a.ProfileInfoId, map => map.Ignore())
            .ForMember(a => a.ProfileInfo, map => map.Ignore())
            .ForMember(a => a.FilesAchievement, map => map.Ignore());
    }
}

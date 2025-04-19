using AutoMapper;
using ProfileService.Application.Contracts.FileAchievementContracts;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services.Mapping;

public class FileAchievementMappingsProfile : Profile
{
    public FileAchievementMappingsProfile()
    {
        CreateMap<FileAchievement, FileAchievementDto>();

        CreateMap<FileAchievementDto, FileAchievement>()
            .ForMember(f => f.Achievement, map => map.Ignore());

        CreateMap<CreatingFileAchievementDto, FileAchievement>()
            .ForMember(f => f.Id, map => map.Ignore())
            .ForMember(f => f.AchievementId, map => map.Ignore())
            .ForMember(f => f.Achievement, map => map.Ignore());
    }
}
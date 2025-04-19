using AutoMapper;
using ProfileService.API.Models.AchievementModels;
using ProfileService.Application.Contracts.AchievementContracts;

namespace ProfileService.API.Mapping;

public class AchievementMappingsProfile : Profile
{
    public AchievementMappingsProfile()
    {
        CreateMap<AchievementModel, AchievementDto>();
        CreateMap<AchievementDto, AchievementModel>();
        CreateMap<CreatingAchievementWithFilesModel, CreatingAchievementWithFilesDto>();
        CreateMap<CreatingAchievementModel, CreatingAchievementDto>();
        CreateMap<UpdatingAchievementModel, UpdatingAchievementDto>();
    }
}

using AutoMapper;
using ProfileService.API.Models.FileAchievementModels;
using ProfileService.Application.Contracts.FileAchievementContracts;

namespace ProfileService.API.Mapping;

public class FileAchievementMappingsProfile : Profile
{
    public FileAchievementMappingsProfile()
    {
        CreateMap<FileAchievementModel, FileAchievementDto>();
        CreateMap<FileAchievementDto, FileAchievementModel>();
        CreateMap<CreatingFileAchievementModel, CreatingFileAchievementDto>();
    }    
}

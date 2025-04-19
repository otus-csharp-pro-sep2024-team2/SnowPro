using AutoMapper;
using ProfileService.API.Models.LevelTrainingModels;
using ProfileService.Application.Contracts.LevelTrainingContracts;

namespace ProfileService.API.Mapping;

public class LevelTrainingMappingsProfile: Profile
{
    public LevelTrainingMappingsProfile()
    {
        CreateMap<LevelTrainingModel, LevelTrainingDto>();
        CreateMap<LevelTrainingDto, LevelTrainingModel>();
        CreateMap<CreatingLevelTrainingModel, CreatingLevelTrainingDto>();
        CreateMap<UpdatingLevelTrainingModel, UpdatingLevelTrainingDto>();
    }
}

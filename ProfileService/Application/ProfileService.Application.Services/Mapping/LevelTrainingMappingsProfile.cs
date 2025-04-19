using AutoMapper;
using ProfileService.Application.Contracts.LevelTrainingContracts;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services.Mapping;
/// <summary>
/// Профиль автомаппера для сущности уровня подготовки.
/// </summary>
public class LevelTrainingMappingsProfile: Profile
{
    public LevelTrainingMappingsProfile()
    {
        CreateMap<LevelTraining, LevelTrainingDto>();
        CreateMap<LevelTrainingDto, LevelTraining>()
            .ForMember(l => l.TypeSportEquipmentProfile, map => map.Ignore());
        CreateMap<CreatingLevelTrainingDto, LevelTraining>()
            .ForMember(l => l.Id, map => map.Ignore())
            .ForMember(l => l.TypeSportEquipmentProfile, map => map.Ignore());
        CreateMap<UpdatingLevelTrainingDto, LevelTraining>()
            .ForMember(l => l.Id, map => map.Ignore())
            .ForMember(l => l.TypeSportEquipmentProfile, map => map.Ignore());
    }
}

using AutoMapper;
using ProfileService.API.Models.PositionModels;
using ProfileService.Application.Contracts.PositionContracts;

namespace ProfileService.API.Mapping;

public class PositionMappingsProfile: Profile
{
    public PositionMappingsProfile()
    {
        CreateMap<PositionModel, PositionDto>();
        CreateMap<PositionDto, PositionModel>();
        CreateMap<CreatingPositionModel, CreatingPositionDto>();
        CreateMap<UpdatingPositionModel, UpdatingPositionDto>();
    }
}

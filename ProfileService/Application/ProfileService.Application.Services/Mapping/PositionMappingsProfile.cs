using AutoMapper;
using ProfileService.Application.Contracts.PositionContracts;
using ProfileService.Domain.Entities;

namespace ProfileService.Application.Services.Mapping;
/// <summary>
/// Профиль автомаппера для сущности должности.
/// </summary>
public class PositionMappingsProfile: Profile
{
    public PositionMappingsProfile()
    {
        CreateMap<Position, PositionDto>();
        CreateMap<PositionDto, Position>();
        CreateMap<CreatingPositionDto, Position>()
            .ForMember(p => p.Id, map => map.Ignore());
        CreateMap<UpdatingPositionDto, Position>()
            .ForMember(p => p.Id, map => map.Ignore());
    }
}
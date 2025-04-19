using AutoMapper;
using ProfileService.API.Models.ClientProfileInfoModels;
using ProfileService.Application.Contracts.ClientProfileInfoContracts;

namespace ProfileService.API.Mapping;

public class ClientProfileInfoMappingsProfile : Profile
{
    public ClientProfileInfoMappingsProfile()
    {
        CreateMap<ClientProfileInfoModel, ClientProfileInfoDto>();
        CreateMap<ClientProfileInfoDto, ClientProfileInfoModel>();
        CreateMap<CreatingClientProfileInfoModel, CreatingClientProfileInfoDto>();
        CreateMap<UpdatingClientProfileInfoModel, UpdatingClientProfileInfoDto>();
    }
}
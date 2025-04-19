using AutoMapper;
using ProfileService.API.Models.ProfileInfoModels;
using ProfileService.Application.Contracts.ProfileInfoContracts;
using SnowPro.Shared.Contracts;

namespace ProfileService.API.Mapping;

public class ProfileInfoMappingsProfile : Profile
{
    public ProfileInfoMappingsProfile()
    {
        CreateMap<ProfileInfoModel, ProfileInfoDto>();
        CreateMap<ProfileInfoDto, ProfileInfoModel>();
        CreateMap<CreatingProfileInfoModel, CreatingProfileInfoDto>();
        CreateMap<UpdatingProfileInfoModel, UpdatingProfileInfoDto>();

        CreateMap<CreatingProfileInfoModel, SharedProfileInfoDto>()
            .ForMember(p => p.Gender, map => map.MapFrom(p => (int?)p.Gender))
            .ForMember(p => p.Id, map => map.Ignore())
            .ForMember(p => p.UserId, map => map.Ignore())
            .ForMember(p => p.CreatedDate, map => map.Ignore())
            .ForMember(p => p.UpdatedDate, map => map.Ignore())
            .ForMember(p => p.Status, map => map.Ignore())
            .ForMember(p => p.IsActive, map => map.Ignore())
            .ForMember(p => p.IsDeleted, map => map.Ignore())
            .ForMember(p => p.UpdatedUserId, map => map.Ignore())
        ;
    }
}

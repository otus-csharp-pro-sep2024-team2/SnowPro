using AutoMapper;
using ProfileService.API.Models.InstructorProfileInfoModels;
using ProfileService.Application.Contracts.InstructorProfileInfoContracts;

namespace ProfileService.API.Mapping;

public class InstructorProfileInfoMappingsProfile : Profile
{
    public InstructorProfileInfoMappingsProfile()
    {
        CreateMap<InstructorProfileInfoModel, InstructorProfileInfoDto>();
        CreateMap<InstructorProfileInfoDto, InstructorProfileInfoModel>();
        CreateMap<CreatingInstructorProfileInfoModel, CreatingInstructorProfileInfoDto>();
        CreateMap<UpdatingInstructorProfileInfoModel, UpdatingInstructorProfileInfoDto>();
    }
}

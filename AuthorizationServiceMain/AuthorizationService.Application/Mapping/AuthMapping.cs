using AuthorizationService.Domain.Models;
using AutoMapper;
using SnowPro.Shared.Contracts;

namespace AuthorizationService.Application.Mapping;
public class AuthMapping : Profile
{
    public AuthMapping()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, UserRegisteredDto>();
        CreateMap<UserDto, NotificationMessageDto>();
    }
}

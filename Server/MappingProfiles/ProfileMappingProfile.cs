using AutoMapper;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;

public class ProfileMappingProfile : Profile
{
    public ProfileMappingProfile()
    {
        CreateMap<ApplicationUser, UserProfileDto>().ReverseMap();
        CreateMap<UpdateUserProfileDto, ApplicationUser>();
    }
}

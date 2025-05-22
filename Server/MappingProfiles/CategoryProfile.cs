using AutoMapper;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;

namespace BlazorBookHub.Server.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}

using AutoMapper;
using BlazorBookHub.Server.Models;
using BlazorBookHub.Shared.Models;

namespace BlazorBookHub.Server.MappingProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(
                    dest => dest.CategoryName, 
                    opt => opt.MapFrom(src => src.Category!.Name));

            CreateMap<CreateBookDto, Book>();
        }
    }
}

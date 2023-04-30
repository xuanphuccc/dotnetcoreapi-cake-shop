using AutoMapper;
using dotnetcoreapi_cake_shop.Dtos;
using dotnetcoreapi_cake_shop.Entities;

namespace dotnetcoreapi_cake_shop.Profiles
{
    public class CategoryProfiles : Profile
    {
        public CategoryProfiles()
        {

            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CategoryRequestDto, Category>()
                .ForMember(
                    dest => dest.CreateAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow)
                 );
        }
    }
}

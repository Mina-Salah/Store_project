using AutoMapper;
using Store.API.Dtos;
using Store.Data.Entity;

namespace Store.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping Product entity to ProductToReturnDto
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name)) // Map Brand Name
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name)) // Map Category Name
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>()); // Resolve Picture URL
        }
    }
}

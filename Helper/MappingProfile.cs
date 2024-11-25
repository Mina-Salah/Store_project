using AutoMapper;
using Store.API.Dtos;
using Store.Data.Entity;

namespace Store.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d=>d.Category,option=>option.MapFrom(s=>s.Category.Name));
            
        }
    }
}

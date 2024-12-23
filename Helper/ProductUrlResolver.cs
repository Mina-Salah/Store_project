using AutoMapper;
using Store.API.Dtos;
using Store.Data.Entity;

namespace Store.API.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_config["BaseURL"]}{source.PictureUrl}";
            }

            return null;
        }
    }
}

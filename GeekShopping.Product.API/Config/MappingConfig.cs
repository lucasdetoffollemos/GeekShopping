using AutoMapper;
using GeekShopping.Product.API.Data.ValueObjects;
using GeekShopping.Product.API.Model;

namespace GeekShopping.Product.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, ProductEntity>();
                config.CreateMap<ProductEntity, ProductVO>();
            });

            return mappingConfig;
        }
    }
}

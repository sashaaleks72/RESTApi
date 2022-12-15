using AutoMapper;

namespace Net7.WebApi.Test.MapConfigs
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Teapot, TeapotEntity>();
            CreateMap<TeapotEntity, TeapotResponse>();
        }
    }
}

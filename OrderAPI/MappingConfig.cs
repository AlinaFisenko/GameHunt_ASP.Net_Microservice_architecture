using AutoMapper;
using OrderAPI.Models;
using OrderAPI.Models.Dto;

namespace OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Order, OrderDto>();
                config.CreateMap<OrderDto, Order>();
                config.CreateMap<Request, RequestDto>();
                config.CreateMap<RequestDto, Request>();
            });
            return mappingConfig;
        }
    }
}

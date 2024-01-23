using AutoMapper;
using AuthAPI.Models;
using AuthAPI.Models.Dto;

namespace AuthAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ApplicationUser, UserDto>();
                config.CreateMap<UserDto, ApplicationUser>();
            });
            return mappingConfig;
        }
    }
}

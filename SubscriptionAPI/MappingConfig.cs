using AutoMapper;
using SubscriptionAPI.Models;
using SubscriptionAPI.Models.Dto;

namespace SubscriptionAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Subscription, SubscriptionDto>();
                config.CreateMap<SubscriptionDto, Subscription>();
                config.CreateMap<Payment_History, Payment_HistoryDto>();
                config.CreateMap<Payment_HistoryDto, Payment_History>();
                config.CreateMap<User_Subscription, User_SubscriptionDto>();
                config.CreateMap<User_SubscriptionDto, User_Subscription>();


            });
            return mappingConfig;
        }
    }
}

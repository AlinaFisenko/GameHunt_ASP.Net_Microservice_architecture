using GameHuntWeb.Models;
using GameHuntWeb.Service.IService;
using GameHuntWeb.Utility;
using SubscriptionAPI.Models.Dto;

namespace GameHuntWeb.Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IBaseService _baseService;

        public SubscriptionService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreatePaymentHistory(Payment_HistoryDto payment)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = payment,
                Url = SD.SubscriptionAPIBase + "/api/subscription/payment/create"
            });
        }

        public async Task<ResponseDto?> CreateSubscriptionAsync(SubscriptionDto subscriptionDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = subscriptionDto,
                Url = SD.SubscriptionAPIBase + "/api/subscription"
            });
        }

        public async Task<ResponseDto?> DeleteSubscriptionAsync(int subscriptionId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.SubscriptionAPIBase + "/api/subscription/" + subscriptionId
            });
        }

        public async Task<ResponseDto?> GetAllSubscriptionsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.SubscriptionAPIBase + "/api/subscription"
            });
        }

        public async Task<ResponseDto?> GetPaymentByUserId(string id_user)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.SubscriptionAPIBase + "/api/subscription/payment/GetPaymentByUserId/" + id_user
            });
        }

        public async Task<ResponseDto?> GetSubscriptionAsync(string title)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.SubscriptionAPIBase + "/api/subscription/GetByTitle/" + title
            });
        }

        public async Task<ResponseDto?> GetSubscriptionByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.SubscriptionAPIBase + "/api/subscription/" + id
            });
        }

        public async Task<ResponseDto?> GetUserSubscriptionByUserId(string id_user)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.SubscriptionAPIBase + "/api/subscription/userSubscription/GetUserSubscriptionByUserId/" + id_user
            });
        }
        
        public async Task<ResponseDto?> SubscribtionCheck(string id_user)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.SubscriptionAPIBase + "/api/subscription/userSubscription/SubscribtionCheck"
            });
        }

        public async Task<ResponseDto?> UpdateSubscriptionAsync(SubscriptionDto subscriptionDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = subscriptionDto,
                Url = SD.SubscriptionAPIBase + "/api/subscription"
            });
        }

        public async Task<ResponseDto?> UpdateUserSubscription(User_SubscriptionDto userSub)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = userSub,
                Url = SD.SubscriptionAPIBase + "/api/subscription/userSubscription"
            });
        }
        
    }
}

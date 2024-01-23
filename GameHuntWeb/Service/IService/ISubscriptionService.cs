using GameHuntWeb.Models;
using SubscriptionAPI.Models.Dto;

namespace GameHuntWeb.Service.IService
{
    public interface ISubscriptionService
    {
        Task<ResponseDto?> GetAllSubscriptionsAsync();
        Task<ResponseDto?> GetSubscriptionByIdAsync(int id);
        Task<ResponseDto?> GetSubscriptionAsync(string title);
        Task<ResponseDto?> CreateSubscriptionAsync(SubscriptionDto subscriptionDto);
        Task<ResponseDto?> UpdateSubscriptionAsync(SubscriptionDto subscriptionDto);
        Task<ResponseDto?> DeleteSubscriptionAsync(int subscriptionId);

        Task<ResponseDto?> CreatePaymentHistory(Payment_HistoryDto payment);
        Task<ResponseDto?> UpdateUserSubscription(User_SubscriptionDto userSub);
        Task<ResponseDto?> GetUserSubscriptionByUserId(string id_user);
        Task<ResponseDto?> GetPaymentByUserId(string id_user);
        Task<ResponseDto?> SubscribtionCheck(string id_user);

    }
}

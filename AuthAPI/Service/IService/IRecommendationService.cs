using AuthAPI.Models.Dto;

namespace AuthAPI.Service.IService
{
    public interface IRecommendationService
    {
        Task<bool> RankingCreate(string devId);
    }
}

using GameHuntWeb.Models;
using GameHuntWeb.Models.Dto;
using GameHuntWeb.Service.IService;
using GameHuntWeb.Utility;
using SubscriptionAPI.Models.Dto;

namespace GameHuntWeb.Service
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IBaseService _baseService;

        public RecommendationService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCommentAsync(CommentDto commentDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = commentDto,
                Url = SD.RecommendationAPIBase + "/api/recommendation"
            });
        }

        public async Task<ResponseDto?> GetAllCommentsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.RecommendationAPIBase + "/api/recommendation"
            });
        }

        public async Task<ResponseDto?> GetCommentByIdClientAsync(string id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.RecommendationAPIBase + "/api/recommendation/GetByIdClient/" + id
            });
        }

        public async Task<ResponseDto?> GetCommentByIdDevAsync(string id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.RecommendationAPIBase + "/api/recommendation/" + id
            });
        }

        public async Task<ResponseDto?> GetDeveloperRating(string id_dev)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.RecommendationAPIBase + "/api/recommendation/GetDeveloperRating/"+id_dev
            });
        }

        public async Task<ResponseDto?> GetRankingByIdDev(string id_dev)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.RecommendationAPIBase + "/api/recommendation/ranking/"+id_dev
            });
        }

        public async Task<ResponseDto?> RankingCreate(string id_dev)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = id_dev,
                Url = SD.RecommendationAPIBase + "/api/recommendation/ranking"
            });
        }

        public async Task<ResponseDto?> RankingUpdate(RankingDto dto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.RecommendationAPIBase + "/api/recommendation/ranking"
            });
        }

        
    }
}

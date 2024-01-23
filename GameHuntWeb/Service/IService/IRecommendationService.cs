using GameHuntWeb.Models;
using GameHuntWeb.Models.Dto;

namespace GameHuntWeb.Service.IService
{
    public interface IRecommendationService
    {
        Task<ResponseDto?> GetAllCommentsAsync();
        Task<ResponseDto?> GetCommentByIdDevAsync(string id);
        Task<ResponseDto?> GetCommentByIdClientAsync(string id);
        Task<ResponseDto?> CreateCommentAsync(CommentDto commentDto);
        Task<ResponseDto?> RankingCreate(string id_dev);
        Task<ResponseDto?> RankingUpdate(RankingDto dto);
        Task<ResponseDto?> GetRankingByIdDev(string id_dev);
        Task<ResponseDto?> GetDeveloperRating(string id_dev);

    }
}

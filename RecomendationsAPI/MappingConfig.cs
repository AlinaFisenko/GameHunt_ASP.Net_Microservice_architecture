using AutoMapper;
using RecomendationsAPI.Models;
using RecomendationsAPI.Models.Dto;

namespace RecomendationsAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Comment, CommentDto>();
                config.CreateMap<CommentDto, Comment>();
                config.CreateMap<Ranking, RankingDto>();
                config.CreateMap<RankingDto, Ranking>();

            });
            return mappingConfig;
        }
    }
}

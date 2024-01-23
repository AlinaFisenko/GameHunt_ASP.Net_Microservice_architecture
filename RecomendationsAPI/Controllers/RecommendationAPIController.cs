using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecomendationsAPI.Data;
using RecomendationsAPI.Models;
using RecomendationsAPI.Models.Dto;

namespace RecommendationAPI.Controllers
{
    [ApiController]
    [Route("api/recommendation")]
    public class RecommendationAPIController : Controller
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mappper;
        public RecommendationAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mappper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Comment> comments = _db.Comments.ToList();
                _response.Result = _mappper.Map<IEnumerable<CommentDto>>(comments);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public ResponseDto GetByIdDev(string id)
        {
            try
            {
                IEnumerable<Comment> comment = _db.Comments.Where(l => l.id_dev == id);
                _response.Result = _mappper.Map<List<CommentDto>>(comment);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByIdClient/{id}")]
        public ResponseDto GetByIdClient(string id)
        {
            try
            {
                Comment comment = _db.Comments.First(l => l.id_client == id);
                _response.Result = _mappper.Map<CommentDto>(comment);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CommentDto commentDto)
        {
            try
            {
                Comment comment = _mappper.Map<Comment>(commentDto);
                var ranking = _db.Rankings.FirstOrDefault(s => s.id_dev == comment.id_dev);
                int id_rank;
                if (ranking != null)
                {
                    id_rank = ranking.id_ranking;
                    comment.id_ranking = id_rank;
                }
                else
                {
                    Ranking rank = new();
                    rank.id_dev = comment.id_dev;
                    rank.count_done = 0;
                    _db.Rankings.Add(ranking);
                    _db.SaveChanges();

                    var ranking2 = _db.Rankings.FirstOrDefault(s => s.id_dev == comment.id_dev);
                    comment.id_ranking = ranking2.id_ranking;
                }
                //var ranking = _db.Rankings.FirstOrDefault(s => s.id_dev == comment.id_dev).id_ranking;
                _db.Comments.Add(comment);
                _db.SaveChanges();

                _response.Result = _mappper.Map<CommentDto>(comment);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[HttpDelete]
        //[Route("{id:int}")]
        //public ResponseDto Delete(int id)
        //{
        //    try
        //    {
        //        Comment subscription = _db.Comments.First(l => l.subscriptionId == id);
        //        _db.Comments.Remove(subscription);
        //        _db.SaveChanges();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}



        [HttpPost]
        [Route("ranking")] //ПРи регистрации разработчика создается рейтинг
        public ResponseDto RankingCreate(string devId)
        {
            try
            {
                Ranking ranking = new();
                ranking.id_dev = devId;
                ranking.count_done = 0;


                if (!_db.Rankings.Any(s => s.id_dev == ranking.id_dev))
                {
                    //    var newR = _db.Rankings.FirstOrDefault(s => s.id_dev == ranking.id_dev);
                    //    _db.Rankings.Update(ranking);
                    //}
                    //else
                    //{
                    _db.Rankings.Add(ranking);
                    _db.SaveChanges();
                    _response.Result = _mappper.Map<RankingDto>(ranking);
                }
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Route("ranking")]
        public ResponseDto RankingUpdate(RankingDto dto) //При завершении заказа увеличивается рейтинг
        {
            try
            {
                Ranking rank = _mappper.Map<Ranking>(dto);
                _db.Rankings.Update(rank);
                _db.SaveChanges();
                _response.Result = _mappper.Map<RankingDto>(rank);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("ranking/{id}")]
        public ResponseDto GetRanking(string id)
        {
            try
            {
                Ranking rank = _db.Rankings.FirstOrDefault(s => s.id_dev == id);
                _response.Result = _mappper.Map<RankingDto>(rank);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetDeveloperRating/{id}")]
        public ResponseDto GetDeveloperRating(string id)
        {
            try
            {
                List<short> rates = _db.Comments.Where(s => s.id_dev == id).Select(s => s.rate).ToList();
                int count = rates.Count();
                int result = 0;

                if (count != 0)
                {
                    foreach (var item in rates)
                    {
                        result += item;
                    }
                    result = result / count;
                }


                _response.Result = result;
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }





    }
}

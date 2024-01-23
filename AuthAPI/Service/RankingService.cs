using AuthAPI.Models.Dto;
using AuthAPI.Service.IService;
using Newtonsoft.Json;
using System.Text;

namespace AuthAPI.Service
{
    public class RankingService : IRecommendationService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public RankingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> RankingCreate(string devId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Recommendation");

                var requestData = new { devId };

                var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                //var content = new StringContent(JsonConvert.SerializeObject(devId), Encoding.UTF8, "application/json");//ew StringContent(devId, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"api/recommendation/ranking", content);

                var apiContent = await response.Content.ReadAsStringAsync();

                var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                if (resp.IsSuccess)
                {
                    return resp.IsSuccess;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}

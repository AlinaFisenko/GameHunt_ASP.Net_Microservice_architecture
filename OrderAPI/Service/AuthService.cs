using Newtonsoft.Json;
using OrderAPI.Models.Dto;
using OrderAPI.Service.IService;

namespace OrderAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetUserRole(string id)
        {
            var client = _httpClientFactory.CreateClient("Auth");
            var response = await client.GetAsync($"api/auth/GetUserRole/{id}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

            if (resp.IsSuccess)
            {
                return resp.Result.ToString();
            }
            return string.Empty;

        }
    }
}

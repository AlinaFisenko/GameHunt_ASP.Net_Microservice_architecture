using GameHuntWeb.Models;

namespace GameHuntWeb.Service.IService
{
    public interface IBaseService
    {
        public Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);

    }
}

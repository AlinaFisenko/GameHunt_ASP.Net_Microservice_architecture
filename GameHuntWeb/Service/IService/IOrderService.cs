using GameHuntWeb.Models;
using GameHuntWeb.Models.Dto;

namespace GameHuntWeb.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> GetAllOrdersAsync();
        Task<ResponseDto?> ClientOrdersAsync(string id);
        Task<ResponseDto?> GetOrderByIdAsync(int id);
        Task<ResponseDto?> GetOrderByUserAsync(string id);
        Task<ResponseDto?> GetOrderAsync(string title);
        Task<ResponseDto?> CreateOrderAsync(OrderDto orderDto);
        Task<ResponseDto?> UpdateOrderAsync(OrderDto orderDto);
        Task<ResponseDto?> DeleteOrderAsync(int orderId);
        Task<ResponseDto?> GetOrderDevelopers(int orderId);
        Task<ResponseDto?> CreateRequest(MyRequestDto requestDto);
        Task<ResponseDto?> GetRequestByUserTo(string id);
        Task<ResponseDto?> GetRequestById(int id);
        Task<ResponseDto?> PutRequestAsync(MyRequestDto requestDto);
        
        Task<ResponseDto?> GetRequestByUserFrom(string id);

    }
}

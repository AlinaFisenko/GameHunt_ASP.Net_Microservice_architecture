using GameHuntWeb.Models;
using GameHuntWeb.Models.Dto;
using GameHuntWeb.Service.IService;
using GameHuntWeb.Utility;

namespace GameHuntWeb.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;

        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

		public async Task<ResponseDto?> ClientOrdersAsync(string id)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.OrderAPIBase + "/api/order/GetByUser/" + id
			});
		}

		public async Task<ResponseDto?> CreateOrderAsync(OrderDto OrderDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = OrderDto,
                Url = SD.OrderAPIBase + "/api/order"
            });
        }

        public async Task<ResponseDto?> CreateRequest(MyRequestDto requestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = requestDto,
                Url = SD.OrderAPIBase + "/api/order/request"
            });
        }

        public async Task<ResponseDto?> DeleteOrderAsync(int OrderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.OrderAPIBase + "/api/order/" + OrderId
            });
        }

        public async Task<ResponseDto?> GetAllOrdersAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            { 
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase+"/api/order" 
            });
        }


        public async Task<ResponseDto?> GetOrderAsync(string title)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetByTitle/" + title
            });
        }

        public async Task<ResponseDto?> GetOrderByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/" + id
            });
        }

        public async Task<ResponseDto?> GetOrderByUserAsync(string id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetByUser/" + id
            });
        }

        public async Task<ResponseDto?> GetOrderDevelopers(int orderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/orderdevs/" + orderId
            });
        }

        public async Task<ResponseDto?> GetRequestById(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/request/" + id
            });
        }

        public async Task<ResponseDto?> GetRequestByUserFrom(string id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/request/GetByUserFrom/" + id
            });
        }

        public async Task<ResponseDto?> GetRequestByUserTo(string id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/request/GetByUserTo/" + id
            });
        }

        public async Task<ResponseDto?> PutRequestAsync(MyRequestDto requestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = requestDto,
                Url = SD.OrderAPIBase + "/api/order/request"
            });
        }

        public async Task<ResponseDto?> UpdateOrderAsync(OrderDto orderDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = orderDto,
                Url = SD.OrderAPIBase + "/api/order"
            });
        }

    }
}

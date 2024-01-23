using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderAPI.Data;
using OrderAPI.Models;
using OrderAPI.Models.Dto;
using OrderAPI.Service.IService;
//using Org.BouncyCastle.Asn1.Ocsp;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderAPIController : Controller
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mappper;
        private IAuthService _authService;


        private IHttpClientFactory _httpClientFactory;
        public OrderAPIController(AppDbContext db, IMapper mapper, IAuthService authService, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _response = new ResponseDto();
            _mappper = mapper;
            _authService = authService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
		//[Authorize(Roles = "CLIENT")]
		public ResponseDto Get()
        {
            try
            {
                IEnumerable<Order> Orders = _db.Orders.Where(u=>u.state.Equals(Models.State.New)).ToList();
                _response.Result = _mappper.Map<IEnumerable<OrderDto>>(Orders);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
		//[Authorize(Roles = "CLIENT")]
		public ResponseDto Get(int id)
        {
            try
            {
                Order order = _db.Orders.First(l => l.id_order == id);
                _response.Result = _mappper.Map<OrderDto>(order);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByUser/{userId}")]
        public ResponseDto Get(string userId)
        {
            try
            {
                IEnumerable<Order> Orders = _db.Orders.Where(l => l.id_user.Equals(userId)).ToList();
                _response.Result = _mappper.Map<IEnumerable<OrderDto>>(Orders);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        [Authorize(Roles = "CLIENT")]
        public ResponseDto Post([FromBody] OrderDto orderDto)
        {
            try
            {
                Order order = _mappper.Map<Order>(orderDto);
                order.date_created = DateTime.Now;
                order.state = Models.State.New;
                _db.Orders.Add(order);
                _db.SaveChanges();

                _response.Result = _mappper.Map<OrderDto>(order);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
		[Authorize(Roles = "CLIENT")]
		public ResponseDto Put([FromBody] OrderDto OrderDto)
        {
            try
            {
                Order Order = _mappper.Map<Order>(OrderDto);
                _db.Orders.Update(Order);
                _db.SaveChanges();

                _response.Result = _mappper.Map<OrderDto>(Order);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
		//[Authorize(Roles = "CLIENT")]
		public ResponseDto Delete(int id)
        {
            try
            {
                Order Order = _db.Orders.First(l => l.id_order == id);
                _db.Orders.Remove(Order);
                _db.SaveChanges();
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("request")]
        public ResponseDto GetRequests()
        {
            try
            {
                IEnumerable<Request> request = _db.Requests.ToList();
                _response.Result = _mappper.Map<IEnumerable<RequestDto>>(request);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("request/{id:int}")]
        //[Authorize(Roles = "CLIENT")]
        public ResponseDto DeleteRequest(int id)
        {
            try
            {
                Request request = _db.Requests.First(l => l.id_request == id);
                _db.Requests.Remove(request);
                _db.SaveChanges();
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete]
        [Route("orderdevs")]
        //[Authorize(Roles = "CLIENT")]
        public ResponseDto DeleteOrder_Devs()
        {
            try
            {
                _db.Order_Devs.RemoveRange(_db.Order_Devs);
                _db.SaveChanges();
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        [Route("request")]
        public async Task<ResponseDto> PutRequestAsync([FromBody] RequestDto requestDto)
        {
            try
            {
                Request request = _mappper.Map<Request>(requestDto);
                _db.Requests.Update(request);
                _db.SaveChanges();

                if (request.state.Equals(Models.StateRequest.Accepted))
                {
                    Order_Devs order_devs = new Order_Devs();
                    var res = await _authService.GetUserRole(request.id_to);
                    var dev_id = res.Equals("DEVELOPER") ? request.id_to : request.id_from;
                    order_devs.id_user = dev_id;
                    order_devs.id_order = request.id_order;

                    if (!_db.Order_Devs.Any(u=>u.id_order==order_devs.id_order && u.id_user==order_devs.id_user))
                    {
                        _db.Order_Devs.Add(order_devs);
                        _db.SaveChanges();
                    }
                }

                _response.Result = _mappper.Map<RequestDto>(request);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("orderdevs/{id}")]
        public async Task<ResponseDto> GetOrderDevelopers(int id)
        {
            try
            {
                IEnumerable<string> devsList = _db.Order_Devs.Where(l => l.id_order == id).Select(l => l.id_user).ToList();
                _response.Result = devsList;
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]
        [Route("request")]
        public ResponseDto CreateRequest([FromBody] RequestDto requestDto)
        {
            try
            {
                Request request = _mappper.Map<Request>(requestDto);
                request.date = DateTime.Now;
                request.state = Models.StateRequest.Pending;
                _db.Requests.Add(request);
                _db.SaveChanges();
                _response.Result = _mappper.Map<RequestDto>(request);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("request/GetByUserFrom/{userId}")]
        public ResponseDto GetByUserFrom(string userId)
        {
            try
            {
                IEnumerable<Request> requests = _db.Requests.Where(l => l.id_from.Equals(userId)).ToList();
                _response.Result = _mappper.Map<IEnumerable<RequestDto>>(requests);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("request/GetByUserTo/{userId}")]
        public ResponseDto GetByUserTo(string userId)
        {
            try
            {
                IEnumerable<Request> requests = _db.Requests.Where(l => l.id_to.Equals(userId)).ToList();
                _response.Result = _mappper.Map<IEnumerable<RequestDto>>(requests);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("request/GetByOrderId/{orderId}")]
        public ResponseDto GetByOrderId(int orderId)
        {
            try
            {
                IEnumerable<Request> requests = _db.Requests.Where(l => l.id_order.Equals(orderId)).ToList();
                _response.Result = _mappper.Map<IEnumerable<RequestDto>>(requests);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("request/{id:int}")]
        public ResponseDto GetById(int id)
        {
            try
            {
                Request request = _db.Requests.First(l => l.id_request == id);
                _response.Result = _mappper.Map<RequestDto>(request);
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

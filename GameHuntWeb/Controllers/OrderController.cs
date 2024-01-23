using GameHuntWeb.Models;
using GameHuntWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace GameHuntWeb.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;
        public OrderController(IOrderService orderService, IAuthService authService)
        {
            _orderService = orderService;
            _authService = authService;
        }

        public async Task<IActionResult> OrderIndex()
        {
            List<OrderDto> orders = new();
            ResponseDto response = await _orderService.GetAllOrdersAsync();

            if (response != null && response.IsSuccess)
            {
                orders = JsonConvert.DeserializeObject<List<OrderDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(orders);
        }


        public async Task<IActionResult> ClientOrders()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            List<OrderDto> orders = new();
            ResponseDto response = await _orderService.ClientOrdersAsync(userId);

            if (response != null && response.IsSuccess)
            {
                orders = JsonConvert.DeserializeObject<List<OrderDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(orders);
        }

        public async Task<IActionResult> ViewOrder(int id)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            List<string> devs = new();
            List<UserDto> devUser = new List<UserDto>();
            OrderDto order = new();
            ResponseDto response = await _orderService.GetOrderByIdAsync(id);
            if (response != null && response.IsSuccess)
            {
                //order = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Result));

                var resp = await _orderService.GetOrderDevelopers(id);

                if (resp != null && resp.IsSuccess)
                {
                    devs = JsonConvert.DeserializeObject<List<string>>(Convert.ToString(resp.Result));
                    ResponseDto response1 = new();
                    //data
                    foreach (var dev in devs)
                    {
                        response = await _authService.GetById(dev);
                        if (response != null && response.IsSuccess)
                        {
                            var dev1 = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result));
                            devUser.Add(dev1);
                        }
                        else
                        {
                            TempData["Error"] = response?.Message;
                        }
                    }
                }
                else
                {
                    TempData["Error"] = resp?.Message;
                }
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(devUser);
        }

        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> OrderCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderCreate(OrderDto model)
        {
            ModelState.Remove("id_user");
            if (ModelState.IsValid)
            {
                model.id_user = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
                ResponseDto response = await _orderService.CreateOrderAsync(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Order created successfully";
                    return RedirectToAction(nameof(ClientOrders));
                }
                else
                {
                    TempData["Error"] = response?.Message;
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> OrderDelete(int id)
        {
            ResponseDto? response = await _orderService.DeleteOrderAsync(id);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Order deleted successfully";
                return RedirectToAction(nameof(ClientOrders));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }
            return RedirectToAction(nameof(ClientOrders));
        }

        public async Task<IActionResult> OrderEdit(int id)
        {
            ResponseDto? response = await _orderService.GetOrderByIdAsync(id);

            if (response != null && response.IsSuccess)
            {
                OrderDto? order = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Result));
                return View(order);
            }
            else
            {
                TempData["Error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> OrderEdit(OrderDto orderDto)
        {
            ResponseDto? response = await _orderService.UpdateOrderAsync(orderDto);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Order updated successfully";
                return RedirectToAction(nameof(ClientOrders));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }
            return View(orderDto);
        }


        public async Task<IActionResult> SendRequest(string id, string selectedOrder)
        {
            var clientId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            MyRequestDto requestDto = new MyRequestDto();
            requestDto.id_from = clientId;
            requestDto.id_to = id;

            requestDto.id_order = Convert.ToInt32(selectedOrder);

            if (requestDto.id_order == 0 || string.IsNullOrEmpty(selectedOrder))
            {
                TempData["Error"] = "Please select an order";
                return RedirectToAction("Developers", "Auth");
            }

            ResponseDto response = await _orderService.CreateRequest(requestDto);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Request created successfully";
                return RedirectToAction("Developers", "Auth");
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return RedirectToAction("Developers", "Auth");
        }

        public async Task<IActionResult> SendRequestToClient(string id)
        {
            var devId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            MyRequestDto requestDto = new MyRequestDto();
            ResponseDto response = await _orderService.GetOrderByIdAsync(Convert.ToInt32(id));
            
            OrderDto order = new();

            if (response != null && response.IsSuccess)
            {
               order = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response?.Message;
                return RedirectToAction("Index", "Home");
            }

            requestDto.id_from = devId;
            requestDto.id_to = order.id_user;
            requestDto.id_order = order.id_order;

            response = await _orderService.CreateRequest(requestDto);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Request created successfully";
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> MyRequests()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            List<MyRequestDto> requests = new();

            ResponseDto response = await _orderService.GetRequestByUserTo(userId);
            List<OrderDto> orders = new();

            if (response != null && response.IsSuccess)
            {
                requests = JsonConvert.DeserializeObject<List<MyRequestDto>>(Convert.ToString(response.Result));

                foreach (var item in requests)
                {
                    response = await _orderService.GetOrderByIdAsync(item.id_order);
                    if (response != null && response.IsSuccess)
                    {
                        OrderDto order = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Result));
                        orders.Add(order);
                    }
                    else
                        TempData["Error"] = response?.Message;
                }

                var viewListStates = new List<SelectListItem>()
                {
                    new SelectListItem() { Text = StateRequest.Pending.ToString(), Value = StateRequest.Pending.ToString() },
                    new SelectListItem() { Text = StateRequest.Accepted.ToString(), Value = StateRequest.Accepted.ToString() },
                    new SelectListItem() { Text = StateRequest.Completed.ToString(), Value = StateRequest.Completed.ToString() },
                    new SelectListItem() { Text = StateRequest.Problems.ToString(), Value = StateRequest.Problems.ToString() },
                    new SelectListItem() { Text = StateRequest.Rejected.ToString(), Value = StateRequest.Rejected.ToString() }
                };

                ViewBag.StateList = viewListStates;

            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            var newList = (orders.Count != 0 && requests.Count != 0) ? orders.Join(requests, o => o.id_order, r => r.id_order, (o, r) => new { o.title, r.state, r.id_request }).ToList() : null;
            return View(newList);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRequest(int id_request, string selectedState)
        {

            MyRequestDto requestDto = new MyRequestDto();
            ResponseDto response = await _orderService.GetRequestById(id_request);
            
            if (response != null && response.IsSuccess)
            {
                if (Enum.TryParse(selectedState, out StateRequest state))
                {
                    requestDto = JsonConvert.DeserializeObject<MyRequestDto>(Convert.ToString(response.Result));

                    requestDto.state = state;

                    response = await _orderService.PutRequestAsync(requestDto);
                    if (response != null && response.IsSuccess)
                    {
                        TempData["Success"] = "Request updated successfully";
                    }
                    else
                    {
                        TempData["Error"] = response?.Message;
                    }
                    return RedirectToAction(nameof(MyRequests));
                }
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return RedirectToAction(nameof(MyRequests));
        }

        public async Task<IActionResult> SendedRequests()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            List<MyRequestDto> requests = new();

            ResponseDto response = await _orderService.GetRequestByUserFrom(userId);
            List<OrderDto> orders = new();

            if (response != null && response.IsSuccess)
            {
                requests = JsonConvert.DeserializeObject<List<MyRequestDto>>(Convert.ToString(response.Result));

                foreach (var item in requests)
                {
                    response = await _orderService.GetOrderByIdAsync(item.id_order);
                    if (response != null && response.IsSuccess)
                    {
                        OrderDto order = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Result));
                        orders.Add(order);
                    }
                    else
                        TempData["Error"] = response?.Message;
                }

            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            var listOfIdTo = requests.Select(x => x.id_to).ToList();

            List<UserDto> usersTo = new();

            foreach (var item in listOfIdTo)
            {
                response = await _authService.GetById(item);
                if (response != null && response.IsSuccess)
                {
                    UserDto user = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result));
                    usersTo.Add(user);
                }
                else
                    TempData["Error"] = response?.Message;
            }

            var newList = (orders.Count != 0 && requests.Count != 0) ? orders.Join(requests, o => o.id_order, r => r.id_order, (o, r) => new { o.title, r.state, r.id_request, r.id_to }).ToList() : null;

            var newList2 = newList?.Join(usersTo, o => o.id_to, r => r.ID, (o, r) => new { o.title, o.state, o.id_request, r.Name })?.Distinct()?.ToList();

            
            return View(newList2);
        }
    }
}



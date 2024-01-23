using GameHuntWeb.Models;
using GameHuntWeb.Service;
using GameHuntWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GameHuntWeb.Controllers
{
    public class HomeController : Controller
    {
		private readonly IOrderService _orderService;
        private readonly ISubscriptionService _subscriptionService;
		public HomeController(IOrderService orderService, ISubscriptionService subscriptionService)
        {
            _orderService = orderService;
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> Index()
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

        public async Task<IActionResult> ViewSubscription()
        {
            List<SubscriptionDto> subscriptions = new();
            ResponseDto response = await _subscriptionService.GetAllSubscriptionsAsync();

            if (response != null && response.IsSuccess)
            {
                subscriptions = JsonConvert.DeserializeObject<List<SubscriptionDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(subscriptions);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            OrderDto order = new();
            ResponseDto response = await _orderService.GetOrderByIdAsync(id);

            if (response != null && response.IsSuccess)
            {
                order = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(order);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [HttpPost]
        public async Task<IActionResult> SendRequestToClient(string id)
        {
            return RedirectToAction("SendRequestToClient", "Order", new { id = id});
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> PaySubscription(ushort id)
        {
            return RedirectToAction("PaySubscription_Update", "Subscription", new { id = id });
        }

    }
}
using GameHuntWeb.Models;
using GameHuntWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SubscriptionAPI.Models.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace GameHuntWeb.Controllers
{
    public class SubscriptionController : Controller
    {

        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> SubscriptionIndex()
        {
            List<SubscriptionDto> subscriptions = new();
            ResponseDto response = await _subscriptionService.GetAllSubscriptionsAsync();

            if(response != null && response.IsSuccess)
            {
                subscriptions = JsonConvert.DeserializeObject<List<SubscriptionDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(subscriptions);
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> SubscriptionCreate()
        {
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> SubscriptionCreate(SubscriptionDto model)
        {
            if(ModelState.IsValid)
            {
                ResponseDto response = await _subscriptionService.CreateSubscriptionAsync(model);
                if(response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Subscription created successfully";
                    return RedirectToAction(nameof(SubscriptionIndex));
                }
				else
				{
					TempData["Error"] = response?.Message;
				}
			}

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubscriptionDelete(int id)
        {
			ResponseDto? response = await _subscriptionService.DeleteSubscriptionAsync(id);

			if (response != null && response.IsSuccess)
			{
                TempData["Success"] = "Subscription deleted successfully";
                return RedirectToAction(nameof(SubscriptionIndex));
			}
			else
			{
				TempData["Error"] = response?.Message;
			}
			return RedirectToAction(nameof(SubscriptionIndex));
		}


        public async Task<IActionResult> PaySubscription_Update(ushort id)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            Payment_HistoryDto payment = new();
            payment.id_subscription = id;
            payment.id_user = userId;

            ResponseDto response = await _subscriptionService.CreatePaymentHistory(payment);

            if (response != null && response.IsSuccess)
            {
                User_SubscriptionDto userSub = new();
                userSub.id_subscription = id;
                userSub.id_user = userId;
                
                response = await _subscriptionService.UpdateUserSubscription(userSub);

                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Subscription paid successfully.";
                    return RedirectToAction("ViewSubscription", "Home");
                }
                else
                {
                    TempData["Error"] = response?.Message;
                }
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return RedirectToAction("ViewSubscription", "Home");

        }
    }
}



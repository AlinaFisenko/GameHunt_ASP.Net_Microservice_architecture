using GameHuntWeb.Models;
using GameHuntWeb.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace GameHuntWeb.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRecommendationService _recommendationService;
        public RecommendationController(IAuthService authService,IOrderService orderService, ISubscriptionService subscriptionService, IRecommendationService recommendationService)
        {
            _authService = authService;
            _orderService = orderService;
            _subscriptionService = subscriptionService;
            _recommendationService = recommendationService;
        }

        public async Task<IActionResult> ViewCommentsAsync(string id)
        {
            ResponseDto responseDto = await _authService.GetById(id);
            UserDto userDto = new();
            if (responseDto != null && responseDto.IsSuccess)
            {
                userDto = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(responseDto.Result));
            }

            ViewBag.User = userDto;

            responseDto = await _recommendationService.GetCommentByIdDevAsync(id);

            List<CommentDto> commentDto = new();

            if (responseDto != null && responseDto.IsSuccess)
            {
                commentDto = JsonConvert.DeserializeObject<List<CommentDto>>(Convert.ToString(responseDto.Result));
            }

            return View(commentDto);
        }

        public async Task<IActionResult> WriteComment(string id)
        {
            CommentDto comment = new ();
            comment.id_client = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            comment.id_dev = id;
            return View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentDto comment)
        {
            ResponseDto responseDto = await _recommendationService.CreateCommentAsync(comment);
            if (responseDto != null && responseDto.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}

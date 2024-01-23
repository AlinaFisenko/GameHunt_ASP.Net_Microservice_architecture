using GameHuntWeb.Models;
using GameHuntWeb.Models.Dto;
using GameHuntWeb.Service.IService;
using GameHuntWeb.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SubscriptionAPI.Models.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GameHuntWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        private readonly IOrderService _orderService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IRecommendationService _recommendationService;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider, IOrderService orderService, ISubscriptionService subscriptionService, IRecommendationService recommendationService)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
            _orderService = orderService;
            _subscriptionService = subscriptionService;
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();
            return View(loginRequestDto);
        }

        [HttpGet]
        public async Task<IActionResult> Developers()
        {
            ResponseDto responseDto = await _authService.GetAllDevelopersAsync();
            if (responseDto != null && responseDto.IsSuccess)
            {
                List<UserDto> developerDto = JsonConvert.DeserializeObject<List<UserDto>>(Convert.ToString(responseDto.Result));

                var currentUserId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
                var currentOrders = await _orderService.ClientOrdersAsync(currentUserId);
                List<OrderDto> orders = new();
                if (currentOrders != null && currentOrders.IsSuccess)
                {
                    orders = JsonConvert.DeserializeObject<List<OrderDto>>(Convert.ToString(currentOrders.Result));
                    var orderList = new List<SelectListItem>();
                    foreach (var item in orders)
                        orderList.Add(new SelectListItem() { Text = item.title, Value = item.id_order.ToString() });

                    ViewBag.OrderList = orderList;
                }

                List<string> idsToRemove = new();

                foreach (var item in developerDto)
                {
                    var userSubscription = await _subscriptionService.GetUserSubscriptionByUserId(item.ID);
                    var userSubscriptionDto = JsonConvert.DeserializeObject<User_SubscriptionDto>(Convert.ToString(userSubscription.Result));

                    if (userSubscriptionDto?.end_date.CompareTo(DateTime.Now)<=0 || userSubscriptionDto==null)
                    {
                        idsToRemove.Add(item.ID);
                    }
                }

                developerDto.RemoveAll(x => idsToRemove.Contains(x.ID));

                Dictionary<string,int> ratesDto = new();
                foreach (var item in developerDto)
                {
                    var rates = await _recommendationService.GetDeveloperRating(item.ID);
                    var res = JsonConvert.DeserializeObject<int>(Convert.ToString(rates.Result));
                    ratesDto.Add(item.ID, res);
                }

                Dictionary<string, short> donesDto = new();
                foreach (var item in developerDto)
                {
                    var dones = await _recommendationService.GetRankingByIdDev(item.ID);
                    var res = JsonConvert.DeserializeObject<RankingDto>(Convert.ToString(dones.Result));
                    donesDto.Add(item.ID, res.count_done);
                }

                var zip = ratesDto.Zip(donesDto, (r, d) => new { r.Key, rate = r.Value, count_done = d.Value }).ToList();
                var newList2 = developerDto.Join(zip, d => d.ID, z => z.Key, (d, z) => new { d.Email, d.ID, d.Name, d.PhoneNumber, z.rate, z.count_done}).ToList();

                return View(newList2);
            }
            else
            {
                TempData["error"] = responseDto.Message;
                return View();
            }
        }


        public async Task<IActionResult> AddComment(string id)
        {
            return RedirectToAction("ViewComments", "Recommendation", new { id = id});
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _authService.LoginAsync(obj);

            if (responseDto != null && responseDto.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = responseDto.Message;
                return View(obj);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem(){Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem(){Text = SD.RoleClient, Value = SD.RoleClient},
                new SelectListItem(){Text = SD.RoleDeveloper, Value = SD.RoleDeveloper}
            };
            ViewBag.RoleList = roleList;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assignRole;

            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleClient;
                }

                assignRole = await _authService.AssignRoleAsync(obj);

                if (assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = result.Message;
            }


            var roleList = new List<SelectListItem>()
            {
                new SelectListItem(){Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem(){Text = SD.RoleClient, Value = SD.RoleClient},
                new SelectListItem(){Text = SD.RoleDeveloper, Value = SD.RoleDeveloper}
            };

            ViewBag.RoleList = roleList;
            return View(obj);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
               jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
               jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


            identity.AddClaim(new Claim(ClaimTypes.Name,
               jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));


            identity.AddClaim(new Claim(ClaimTypes.Role,
               jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));


            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                               CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }


        [HttpPost]
        public async Task<IActionResult> SendRequest(string id_user, string selectedOrder)
        {
            return RedirectToAction("SendRequest", "Order", new { id = id_user, selectedOrder = selectedOrder });
        }
    }
}


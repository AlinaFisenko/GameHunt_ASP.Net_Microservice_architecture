using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Models.Dto;
using AuthAPI.Service.IService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : Controller
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;
        private readonly IRecommendationService _recommendationService;
        //
        private readonly AppDbContext _db;
        private IMapper _mappper;

        public AuthAPIController(IAuthService authService, AppDbContext db, IMapper mapper, IRecommendationService recommendationService)
        {
            _authService = authService;
            _response = new ResponseDto();
            _db = db;
            _mappper = mapper;
            _recommendationService = recommendationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequestDto model )
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            //var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == model.Email).Id;
            
            //if (model.Role.Equals("DEVELOPER"))
            //{
            //    var res = _recommendationService.RankingCreate(user);
            //}

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto model)  
        {
           var loginResponseDto = await _authService.Login(model);
            if (loginResponseDto.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid login or username";
                return BadRequest(_response);
              }
            _response.Result = loginResponseDto;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                var userIds = _db.UserRoles.Where(ur => ur.RoleId == "6a08a7bc-0983-4e94-9675-37866c580fe7").Select(ur => ur.UserId).ToList();
                var users = _db.ApplicationUsers.Where(u => userIds.Contains(u.Id)).ToList();

                _response.Result = _mappper.Map<IEnumerable<UserDto>>(users);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public ResponseDto GetById(string id)
        {
            try
            {
                var user = _db.ApplicationUsers.FirstOrDefault(u => u.Id==id);
                _response.Result = _mappper.Map<UserDto>(user);
            }
            catch (System.Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetUserRole/{id}")]
        public ResponseDto GetUserRole(string id)
        {
            try
            {
                var roleId = _db.UserRoles.FirstOrDefault(ur => ur.UserId == id).RoleId;
                var roleName = _db.Roles.FirstOrDefault(u =>u.Id== roleId).Name;

                _response.Result = roleName;
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

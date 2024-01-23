using GameHuntWeb.Models;
using GameHuntWeb.Models.Dto;

namespace GameHuntWeb.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> GetAllDevelopersAsync();
        Task<ResponseDto?> GetById(string id);

    }
}

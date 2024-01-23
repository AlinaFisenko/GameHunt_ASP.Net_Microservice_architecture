namespace OrderAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string>GetUserRole(string userId);
    }
}

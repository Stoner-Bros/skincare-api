using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;

namespace APP.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> Login(LoginRequest login);
        Task<AuthResponse?> RefreshToken(string refreshToken);
        Task<bool> Logout(string id, LogoutRequest request);
        Task<bool> IsValidToken(string token);
        Task<AccountResponse?> GetProfile(string id);
    }
}

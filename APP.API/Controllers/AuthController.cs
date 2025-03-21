using APP.API.Controllers.Helper;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APP.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;


        public AuthController(IAuthService authService, IAccountService accountService)
        {
            _authService = authService;
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginRequest login)
        {
            AuthResponse? data = await _authService.Login(login);

            if (data != null)
            {
                return ResponseOk(data);
            }
            return ResponseNoData(401, "Invalid email or password.");
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Register([FromBody] AccountCreationRequest request)
        {
            if (await _accountService.AccountExists(request.Email))
            {
                return ResponseNoData(409, "Email already exist");
            }

            AuthResponse? data = await _authService.Register(request);

            if (data != null)
            {
                return ResponseOk(data);
            }
            return ResponseNoData(400, "Bad Request");
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> RefreshToken([FromBody] string refreshToken)
        {
            AuthResponse? data = await _authService.RefreshToken(refreshToken);

            if (data != null)
            {
                return ResponseOk(data);
            }
            return ResponseNoData(400, "Invalid refresh token.");
        }

        [HttpGet("validate-token")]
        public async Task<ActionResult<ApiResponse>> ValidateToken([FromQuery] string token)
        {
            if (await _authService.IsValidToken(token))
            {
                return ResponseOk();
            }
            return ResponseNoData(400, "Invalid token.");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<ApiResponse>> Logout([FromBody] LogoutRequest request)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return ResponseNoData(401, "Unauthorized");
            }
            bool response = await _authService.Logout(id, request);

            if (response)
            {
                return ResponseOk();
            }
            return ResponseNoData(400, "Invalid token.");
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<ApiResponse<AccountResponse>>> GetProfile()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return ResponseNoData(401, "Unauthorized");
            }
            AccountResponse? data = await _authService.GetProfile(id);

            if (data != null)
            {
                return ResponseOk(data);
            }
            return ResponseNoData(404, "Not found");
        }

    }
}

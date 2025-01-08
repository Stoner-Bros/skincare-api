﻿using APP.API.Controllers.Helper;
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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginRequest login)
        {
            AuthResponse? data = await _authService.Login(login);

            if (data != null)
            {
                return Ok(HiTechApi.ResponseOk(data));
            }
            return Unauthorized(HiTechApi.ResponseNoData(401, "Invalid email or password."));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<AuthResponse>>> RefreshToken([FromBody] string refreshToken)
        {
            AuthResponse? data = await _authService.RefreshToken(refreshToken);

            if (data != null)
            {
                return Ok(HiTechApi.ResponseOk(data));
            }
            return BadRequest(HiTechApi.ResponseNoData(400, "Invalid refresh token."));
        }

        [HttpGet("validate-token")]
        public async Task<ActionResult<ApiResponse>> ValidateToken([FromQuery] string token)
        {
            if (await _authService.IsValidToken(token))
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseNoData(400, "Invalid token."));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<ApiResponse>> Logout([FromBody] LogoutRequest request)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }
            bool response = await _authService.Logout(id, request);

            if (response)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseNoData(400, "Invalid token."));
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<ApiResponse<AccountResponse>>> GetProfile()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }
            AccountResponse? data = await _authService.GetProfile(id);

            if (data != null)
            {
                return Ok(HiTechApi.ResponseOk(data));
            }
            return NotFound(HiTechApi.ResponseNotFound());
        }

    }
}

using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using APP.Utility;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace APP.BLL.Implements
{
    public class AuthService : IAuthService
    {
        private readonly JwtUtil _jwtUtil;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUnitOfWork unitOfWork, JwtUtil jwtUtil, IMapper mapper, ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            _jwtUtil = jwtUtil;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AuthResponse?> Login(LoginRequest login)
        {
            var account = await _unitOfWork.Accounts.GetByEmailAsync(login.Email);
            if (account == null || account.IsDeleted == true || !PasswordEncoder.Verify(login.Password, account.Password))
            {
                return null;
            }

            var jwtToken = _jwtUtil.GenerateJwtToken(account);
            var refreshToken = _jwtUtil.GenerateRefreshToken();

            bool success = true;
            var refeshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                Expiry = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                AccountId = account.AccountId,
            };

            try
            {
                await _unitOfWork.RefeshTokens.CreateAsync(refeshTokenEntity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                success = false;
                _logger.LogError(ex, "An error occurred while creating the refresh token at {Time}.", DateTime.Now);
            }

            if (!success)
            {
                return null;
            }

            return new AuthResponse { AccessToken = jwtToken, RefreshToken = refreshToken };
        }

        public async Task<AuthResponse?> RefreshToken(string refreshToken)
        {
            var token = await _unitOfWork.RefeshTokens.GetByRefreshTokenAsync(refreshToken);

            if (token == null || !token.IsActive)
                return null;

            var newJwtToken = _jwtUtil.GenerateJwtToken(token.Account);

            var newRefreshToken = _jwtUtil.GenerateRefreshToken();

            bool success = true;
            var refeshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                Expiry = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                AccountId = token.Account.AccountId,
            };

            try
            {
                await _unitOfWork.RefeshTokens.CreateAsync(refeshTokenEntity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                success = false;
                _logger.LogError(ex, "An error occurred while creating the refresh token at {Time}.", DateTime.Now);
            }

            if (!success)
            {
                return null;
            }

            token.Revoked = DateTime.Now;  // Vô hiệu hóa refresh token cũ
            try
            {
                _unitOfWork.RefeshTokens.Update(token);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while disable the old refresh token at {Time}.", DateTime.Now);
                return null;
            }

            return new AuthResponse { AccessToken = newJwtToken, RefreshToken = newRefreshToken };
        }

        public async Task<bool> Logout(string id, LogoutRequest request)
        {
            try
            {
                var accountId = _jwtUtil.GetAccountIdFromToken(request.AccessToken);
                if (accountId == null || !accountId.Equals(id))
                    return false;

                var refeshToken = await _unitOfWork.RefeshTokens.GetByRefreshTokenAsync(request.RefreshToken);
                if (refeshToken == null || !refeshToken.IsActive)
                    return false;

                // Vô hiệu hóa access token
                var token = new ExpiredToken
                {
                    Token = request.AccessToken,
                    InvalidationTime = DateTime.Now,
                    AccountId = Int32.Parse(id),
                };
                await _unitOfWork.ExpiredTokens.CreateAsync(token);

                // Vô hiệu hóa refresh token
                refeshToken.Revoked = DateTime.Now;

                _unitOfWork.RefeshTokens.Update(refeshToken);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logout at {Time}.", DateTime.Now);
                return false;
            }

            return true;
        }

        public async Task<bool> IsValidToken(string token)
        {
            bool result = false;
            try
            {
                if (!_jwtUtil.ValidateToken(token)) return false;

                var revokedToken = await _unitOfWork.ExpiredTokens.GetByIDAsync(token);
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var expiration = jwtToken.ValidTo;

                result = expiration > DateTime.UtcNow && revokedToken == null;
            }
            catch (Exception ex)
            {
                result = false; // error => token invalid
                _logger.LogError(ex, "An error occurred while validate token at {Time}.", DateTime.Now);
            }

            return result;
        }

        public async Task<AccountResponse?> GetProfile(string id)
        {
            var account = await _unitOfWork.Accounts.GetDetailInfoByIDAsync(Int32.Parse(id));

            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AuthResponse?> Register(AccountCreationRequest request)
        {
            var account = _mapper.Map<Account>(request);
            account.Password = PasswordEncoder.Encode(request.Password);
            account.AccountInfo = _mapper.Map<AccountInfo>(request);

            try
            {
                var accountResponse = await _unitOfWork.Accounts.CreateAsync(account);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.Customers.CreateAsync(new Customer
                {
                    AccountId = accountResponse.AccountId
                });
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the account at {Time}.", DateTime.Now);
                return null;
            }
            return await Login(new LoginRequest
            {
                Email = request.Email,
                Password = request.Password,
            });
        }
    }
}

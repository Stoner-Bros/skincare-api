using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers.Helper
{
    /// <summary>
    /// Base controller for other API controllers in the application.
    /// </summary>
    /// <remarks>
    ///		- Uses application/json as the default content type.<br/>
    ///		- Provides utility methods to conveniently return common responses.
    /// </remarks>
    [ApiController]
    [Consumes("application/json")]
    public abstract class ApiBaseController : ControllerBase
    {
        /// <summary>
        /// Convenience method to return a 200 OK response.
        /// </summary>
        /// <returns></returns>
        protected static ObjectResult ResponseOk() => ResponseOk<object>(null);

        /// <summary>
        /// Generic response for "200 OK".
        /// </summary>
        protected static readonly ObjectResult _respOk = ResponseNoData(200, "Ok.");

        /// <summary>
        /// Convenience method to return a 200 OK response with data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static ObjectResult ResponseOk<T>(T? data) => data == null ? _respOk : new OkObjectResult(new ApiResponse<T>
        {
            Status = 200,
            Message = "Ok.",
            Data = data
        });

        /// <summary>
        /// Convenience method to return a 200 OK response with data and message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static ObjectResult ResponseOk<T>(string message, T? data) => data == null ? _respOk : new OkObjectResult(new ApiResponse<T>
        {
            Status = 200,
            Message = message,
            Data = data
        });

        /// <summary>
        /// Convenience method to return a response without attached data.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected static ObjectResult ResponseNoData(int statusCode) => ResponseNoData(statusCode, null);

        /// <summary>
        /// Convenience method to return a response with a message and without attached data.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected static ObjectResult ResponseNoData(int statusCode, string? message) => new(String.IsNullOrWhiteSpace(message) ? new ApiResponse<object>
        {
            Status = statusCode
        }
        : new ApiResponse<object>
        {
            Status = statusCode,
            Message = message
        })
        {
            StatusCode = statusCode
        };

        /// <summary>
        /// Convenience method to return a custom response.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static ObjectResult CustomResponse<T>(int statusCode, string? message, T? data)
            => new(new ApiResponse<T>
            {
                Status = statusCode,
                Message = message,
                Data = data
            });

        /// <summary>
        /// Generic response for "401 Unauthorized" errors.
        /// </summary>
        protected static readonly ObjectResult _respAuthenticationRequired = ResponseNoData(401, "Authentication required.");

        /// <summary>
        /// Generic response for "403 Forbidden" errors.
        /// </summary>
        protected static readonly ObjectResult _respAccessDenied = ResponseNoData(403, "Access denied.");

        /// <summary>
        /// Generic response for "404 Not Found" errors.
        /// </summary>
        protected static readonly ObjectResult _respNotFound = ResponseNoData(404, "Not found.");

        /// <summary>
        /// Generic response for "501 Not Implemented" errors.
        /// </summary>
        protected static readonly ObjectResult _respNotImplemented = ResponseNoData(501, "Not implemented.");

        /// <summary>
        /// Generic response for "503 Service Unavailable" errors.
        /// </summary>
        protected static readonly ObjectResult _respServiceUnavailable = ResponseNoData(503, "Server is unavailable to handle the request.");

        /// <summary>
        /// Retrieve the auth token from the request headers.
        /// </summary>
        /// <returns></returns>
        protected string? GetAuthToken()
        {
            return HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        }

        /// <summary>
        /// Get the attached user-id from the http-context.
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        protected string? GetUserID(IdentityOptions opts)
        {
            return HttpContext.User.Claims.FirstOrDefault(c => c.Type == opts.ClaimsIdentity.UserIdClaimType)?.Value;
        }

        /// <summary>
        /// Get the attached user-name from the http-context.
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        protected string? GetUserName(IdentityOptions opts)
        {
            return HttpContext.User.Claims.FirstOrDefault(c => c.Type == opts.ClaimsIdentity.UserNameClaimType)?.Value;
        }
    }
}

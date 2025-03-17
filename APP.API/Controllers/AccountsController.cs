using APP.API.Controllers.Helper;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ApiBaseController
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/hitech/accounts
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AccountResponse>>>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAsync();
            return ResponseOk(accounts);
        }

        // GET: api/hitech/accounts/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AccountResponse>>> GetAccount(int id)
        {
            var account = await _accountService.GetByIDAsync(id);

            var response = account != null
                ? ResponseOk(account)
                : ResponseNoData(404, "Account Not Found!");

            return response;
        }

        // PUT: api/hitech/accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> PutAccount(int id, AccountUpdationRequest request)
        {
            var response = ResponseNoData(404, "Account Not Found!");

            if (!await _accountService.AccountExists(id))
            {
                return NotFound(response);
            }

            bool flag = await _accountService.UpdateAsync(id, request);

            if (flag)
            {
                return NoContent();
            }
            return ResponseNoData(400, "Bad Request");
        }

        // POST: api/hitech/accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<AccountResponse>>> PostAccount(AccountCreationRequest request)
        {
            var response = ResponseNoData(409, "Account already exist");
            if (await _accountService.AccountExists(request.Email))
            {
                return Conflict(response);
            }

            var account = await _accountService.CreateAsync(request);

            if (account == null)
            {
                return ResponseNoData(400, "Bad Request");
            }

            return CustomResponse(201, "Created.", account);
        }

        // DELETE: api/hitech/accounts/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteAccount(int id)
        {
            var response = ResponseNoData(404, "Account Not Found!");

            if (!await _accountService.AccountExists(id))
            {
                return NotFound(response);
            }

            bool flag = await _accountService.DeleteAsync(id);
            if (flag)
            {
                return NoContent();
            }
            return ResponseNoData(400, "Bad Request");
        }
    }
}

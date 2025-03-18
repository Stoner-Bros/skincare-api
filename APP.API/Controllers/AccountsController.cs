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
        public async Task<ActionResult<ApiResponse<PaginationModel<AccountResponse>>>> GetAccounts(
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");

            var pagedAccounts = await _accountService.GetPagedAsync(pageNumber, pageSize);

            return ResponseOk(pagedAccounts);
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
            if (!await _accountService.AccountExists(id))
            {
                return ResponseNoData(404, "Account Not Found!");
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
            if (await _accountService.AccountExists(request.Email))
            {
                return ResponseNoData(409, "Account already exist");
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
            if (!await _accountService.AccountExists(id))
            {
                return ResponseNoData(404, "Account Not Found!");
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

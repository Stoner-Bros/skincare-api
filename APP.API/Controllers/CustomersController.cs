using APP.API.Controllers.Helper;
using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ApiBaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;

        public CustomersController(ICustomerService customerService, IAccountService account)
        {
            _customerService = customerService;
            _accountService = account;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetCustomers(
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");

            var pagedAccounts = await _customerService.GetAllAsync(pageNumber, pageSize);

            return ResponseOk(pagedAccounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomer(int id)
        {
            var customer = await _customerService.GetByIDAsync(id);
            return customer == null ? _respNotFound : ResponseOk(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> CreateCustomer(AccountCreationRequest request)
        {
            if (await _accountService.AccountExists(request.Email))
            {
                return ResponseNoData(409, "Email already exist");
            }

            var customer = await _customerService.CreateAsync(request);

            if (customer == null)
            {
                return ResponseNoData(400, "Bad Request");
            }

            return CustomResponse(201, "Created.", customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!await _customerService.DeleteAsync(id)) return _respNotFound;
            return NoContent();
        }
    }
}

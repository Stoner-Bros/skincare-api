using APP.API.Controllers.Helper;
using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/staffs")]
    [ApiController]
    public class StaffController : ApiBaseController
    {
        private readonly IStaffService _staffService;
        private readonly IAccountService _accountService;

        public StaffController(IStaffService staffService, IAccountService accountService)
        {
            _staffService = staffService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffResponse>>> GetStaff(
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");

            var pagedAccounts = await _staffService.GetAllAsync(pageNumber, pageSize);

            return ResponseOk(pagedAccounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffResponse>> GetStaffMember(int id)
        {
            var staff = await _staffService.GetByIDAsync(id);
            return staff == null ? _respNotFound : ResponseOk(staff);
        }

        [HttpPost]
        public async Task<ActionResult<StaffResponse>> CreateStaff(StaffCreationRequest request)
        {
            if (await _accountService.AccountExists(request.Email))
            {
                return ResponseNoData(409, "Email already exist");
            }

            var staff = await _staffService.CreateAsync(request);

            if (staff == null)
            {
                return ResponseNoData(400, "Bad Request");
            }

            return CustomResponse(201, "Created.", staff);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, StaffUpdationRequest request)
        {
            if (!await _staffService.UpdateAsync(id, request)) return _respNotFound;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            if (!await _staffService.DeleteAsync(id)) return _respNotFound;
            return NoContent();
        }
    }
}

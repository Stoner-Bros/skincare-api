using APP.API.Controllers.Helper;
using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/skintherapists")]
    [ApiController]
    public class SkinTherapistsController : ApiBaseController
    {
        private readonly ISkinTherapistService _skinTherapistService;
        private readonly IAccountService _accountService;

        public SkinTherapistsController(ISkinTherapistService skinTherapistService, IAccountService accountService)
        {
            _skinTherapistService = skinTherapistService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationModel<SkinTherapistResponse>>> GetSkinTherapists(
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");

            var pagedAccounts = await _skinTherapistService.GetAllAsync(pageNumber, pageSize);

            return ResponseOk(pagedAccounts);
        }

        [HttpGet("free")]
        public async Task<ActionResult<PaginationModel<SkinTherapistResponse>>> GetSkinTherapistsFree(
                [FromQuery] DateOnly? date = null,
                [FromQuery] int[]? timeSlotIds = null,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
    )
        {
            date ??= DateOnly.FromDateTime(DateTime.Today);
            timeSlotIds ??= [1];

            if (timeSlotIds.Length == 0)
                return ResponseNoData(400, "At least one TimeSlotId must be provided.");

            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must be greater than 0.");

            var pagedAccounts = await _skinTherapistService.GetAllFreeInSlotAsync(date, timeSlotIds, pageNumber, pageSize);

            return ResponseOk(pagedAccounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkinTherapistResponse>> GetSkinTherapist(int id)
        {
            var therapist = await _skinTherapistService.GetByIDAsync(id);
            return therapist == null ? _respNotFound : ResponseOk(therapist);
        }

        [HttpPost]
        public async Task<ActionResult<SkinTherapistResponse>> CreateSkinTherapist(SkinTherapistCreationRequest request)
        {
            if (await _accountService.AccountExists(request.Email))
            {
                return ResponseNoData(409, "Email already exist");
            }

            var therapist = await _skinTherapistService.CreateAsync(request);

            if (therapist == null)
            {
                return ResponseNoData(400, "Bad Request");
            }

            return CustomResponse(201, "Created.", therapist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkinTherapist(int id, SkinTherapistUpdationRequest request)
        {
            if (!await _skinTherapistService.UpdateAsync(id, request)) return _respNotFound;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkinTherapist(int id)
        {
            if (!await _skinTherapistService.DeleteAsync(id)) return _respNotFound;
            return NoContent();
        }
    }
}

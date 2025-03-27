using APP.API.Controllers.Helper;
using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using APP.Entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/skin-therapist-schedules")]
    [ApiController]
    public class SkinTherapistSchedulesController : ApiBaseController
    {
        private readonly ISkinTherapistScheduleService _scheduleService;
        private readonly IAccountService _accountService;

        public SkinTherapistSchedulesController(ISkinTherapistScheduleService scheduleService, IAccountService accountService)
        {
            _scheduleService = scheduleService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationModel<SkinTherapistScheduleResponse>>> GetSchedules(
                [FromQuery] int therapistId = 1,
                [FromQuery] DateOnly? date = null,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            date ??= DateOnly.FromDateTime(DateTime.Today);
            var account = await _accountService.GetByIDAsync(therapistId);
            if (account == null || account.Role != "Skin Therapist")
                return ResponseNoData(404, "Skin therapist not found.");

            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");
            var pagedTreatments = await _scheduleService.GetAllAsync(date, therapistId, pageNumber, pageSize);

            return ResponseOk(pagedTreatments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkinTherapistScheduleResponse>> GetSchedule(int id)
        {
            var schedule = await _scheduleService.GetByIDAsync(id);
            return schedule != null ? ResponseOk(schedule) : _respNotFound;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SkinTherapistScheduleResponse>>> PostSchedule(SkinTherapistScheduleCreationRequest request)
        {
            var schedule = await _scheduleService.CreateAsync(request);
            return schedule != null ? ResponseOk(schedule) : _respBadRequest;
        }

        [HttpPost("cancel-schedule")]
        public async Task<IActionResult> CancelSchedule(SkinTherapistScheduleCreationRequest request)
        {
            return await _scheduleService.Disable(request) ? ResponseOk() : _respBadRequest;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, SkinTherapistScheduleUpdationRequest request)
        {
            return await _scheduleService.UpdateAsync(id, request) ? NoContent() : _respNotFound;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            return await _scheduleService.DeleteAsync(id) ? NoContent() : _respNotFound;
        }
    }
}

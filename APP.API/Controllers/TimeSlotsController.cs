using APP.API.Controllers.Helper;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/timeslots")]
    [ApiController]
    public class TimeSlotsController : ApiBaseController
    {
        private readonly ITimeSlotService _timeSlotService;
        public TimeSlotsController(ITimeSlotService timeSlotService) => _timeSlotService = timeSlotService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeSlotResponse>>> GetTimeSlots() => ResponseOk(await _timeSlotService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeSlotResponse>> GetTimeSlot(int id)
        {
            var timeSlot = await _timeSlotService.GetByIDAsync(id);
            return timeSlot == null ? _respNotFound : ResponseOk(timeSlot);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeSlot([FromBody] TimeSlotInitRequest request)
        {
            if (!await _timeSlotService.InitAgainAsync(request)) return ResponseNoData(400, "Bad Request");
            return _respOk;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Toogle(int id, [FromBody] string note)
        {
            if (!await _timeSlotService.Toogle(id, note)) return _respNotFound;
            return _respOk;
        }

        [HttpDelete("multiple")]
        public async Task<IActionResult> DeleteTimeSlot([FromBody] int[] ids)
        {
            if (!await _timeSlotService.DeleteMultipleAsync(ids)) return _respNotFound;
            return _respOk;
        }
    }
}

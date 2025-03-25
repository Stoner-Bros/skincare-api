using APP.API.Controllers.Helper;
using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ApiBaseController
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationModel<object>>> GetBookings(
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");

            var pagedAccounts = await _bookingService.GetAllAsync(pageNumber, pageSize);

            return ResponseOk(pagedAccounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBooking(int id)
        {
            var booking = await _bookingService.GetByIDAsync(id);
            return booking != null ? ResponseOk(booking) : _respNotFound;
        }

        [HttpPost]
        public async Task<IActionResult> PostBooking(BookingCreationRequest request)
        {
            var success = await _bookingService.CreateAsync(request);

            return success ? ResponseOk() : _respBadRequest;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, BookingUpdationRequest request)
        {
            return await _bookingService.UpdateAsync(id, request) ? NoContent() : _respNotFound;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            return await _bookingService.DeleteAsync(id) ? NoContent() : _respNotFound;
        }
    }
}

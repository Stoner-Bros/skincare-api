using APP.API.Controllers.Helper;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APP.API.Controllers
{
    [Route("api/feedbacks")]
    [ApiController]
    public class FeedbackController : ApiBaseController
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService) => _feedbackService = feedbackService;

        [HttpGet]
        public async Task<ActionResult<PaginationModel<FeedbackResponse>>> GetFeedbacks(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");
            var pagedFeedbacks = await _feedbackService.GetAllAsync(pageNumber, pageSize);
            return ResponseOk(pagedFeedbacks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackResponse>> GetFeedback(int id)
        {
            var feedback = await _feedbackService.GetByIDAsync(id);
            return feedback == null ? NotFound() : Ok(feedback);
        }

        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<FeedbackResponse>> GetFeedbackByBookingId(int bookingId)
        {
            var feedback = await _feedbackService.GetByBookingIdAsync(bookingId);
            return feedback == null ? NotFound() : Ok(feedback);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FeedbackResponse>> CreateFeedback(int bookingId, FeedbackCreationRequest request)
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return ResponseNoData(401, "Unauthorized");
            }

            try
            {
                var feedback = await _feedbackService.CreateAsync(bookingId, request, int.Parse(accountId));
                return CreatedAtAction(nameof(GetFeedback), new { id = feedback?.FeedbackId }, feedback);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedback(int id, FeedbackUpdationRequest request)
        {
            if (!await _feedbackService.UpdateAsync(id, request)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            if (!await _feedbackService.DeleteAsync(id)) return NotFound();
            return NoContent();
        }
    }
}


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
    [Route("api/feedback-replies")]
    [ApiController]
    public class FeedbackReplyController : ApiBaseController
    {
        private readonly IFeedbackReplyService _feedbackReplyService;
        public FeedbackReplyController(IFeedbackReplyService feedbackReplyService) => _feedbackReplyService = feedbackReplyService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackReplyResponse>>> GetFeedbackReplies()
        {
            var replies = await _feedbackReplyService.GetAllAsync();
            return ResponseOk(replies);
        }

        [HttpGet("feedback/{feedbackId}")]
        public async Task<ActionResult<IEnumerable<FeedbackReplyResponse>>> GetFeedbackRepliesByFeedbackId(int feedbackId)
        {
            var replies = await _feedbackReplyService.GetByFeedbackIdAsync(feedbackId);
            return ResponseOk(replies);
        }

        [Authorize]
        [HttpPost("feedback/{feedbackId}")]
        public async Task<ActionResult<FeedbackReplyResponse>> CreateFeedbackReply(int feedbackId, FeedbackReplyCreationRequest request)
        {
            var staffId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (staffId == null)
            {
                return ResponseNoData(401, "Unauthorized");
            }

            try
            {
                var reply = await _feedbackReplyService.CreateByFeedbackIdAsync(feedbackId, request, int.Parse(staffId));
                return CreatedAtAction(nameof(GetFeedbackRepliesByFeedbackId), new { feedbackId = reply?.FeedbackId }, reply);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeedbackReply(int id, FeedbackReplyUpdationRequest request)
        {
            if (!await _feedbackReplyService.UpdateAsync(id, request)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackReply(int id)
        {
            if (!await _feedbackReplyService.DeleteAsync(id)) return NotFound();
            return NoContent();
        }
    }
}


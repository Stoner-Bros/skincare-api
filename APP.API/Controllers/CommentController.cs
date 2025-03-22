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
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ApiBaseController
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService) => _commentService = commentService;

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentResponse>> GetComment(int id)
        {
            var comment = await _commentService.GetByIDAsync(id);
            return comment == null ? NotFound() : Ok(comment);
        }

        [Authorize]
        [HttpPost("{blogId}")]
        public async Task<ActionResult<CommentResponse>> CreateComment(int blogId, CommentCreationRequest request)
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return ResponseNoData(401, "Unauthorized");
            }

            var comment = await _commentService.CreateAsync(int.Parse(accountId), blogId, request);
            return CreatedAtAction(nameof(GetComment), new { id = comment?.CommentId }, comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, CommentUpdationRequest request)
        {
            if (!await _commentService.UpdateAsync(id, request)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (!await _commentService.DeleteAsync(id)) return NotFound();
            return NoContent();
        }

        [HttpGet("blog/{blogId}")]
        public async Task<ActionResult<PaginationModel<CommentResponse>>> GetCommentsByBlogId(int blogId, int pageNumber, int pageSize)
        {
            var pagedComments = await _commentService.GetByBlogIdAsync(blogId, pageNumber, pageSize);
            return Ok(pagedComments);
        }
    }
}

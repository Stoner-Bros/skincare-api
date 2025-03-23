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
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : ApiBaseController
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService) => _blogService = blogService;

        [HttpGet]
        public async Task<ActionResult<PaginationModel<BlogResponse>>> GetBlogs(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");
            var pagedBlogs = await _blogService.GetAllAsync(pageNumber, pageSize);
            return ResponseOk(pagedBlogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogResponse>> GetBlog(int id)
        {
            var blog = await _blogService.GetByIDAsync(id);
            return blog == null ? NotFound() : Ok(blog);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BlogResponse>> CreateBlog(BlogCreationRequest request)
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return ResponseNoData(401, "Unauthorized");
            }

            var blog = await _blogService.CreateAsync(int.Parse(accountId), request);
            return CreatedAtAction(nameof(GetBlog), new { id = blog?.BlogId }, blog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, BlogUpdationRequest request)
        {
            if (!await _blogService.UpdateAsync(id, request)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (!await _blogService.DeleteAsync(id)) return NotFound();
            return NoContent();
        }

        [HttpPatch("publish/{id}")]
        public async Task<IActionResult> PublishBlog(int id)
        {
            try
            {
                if (!await _blogService.PublishAsync(id)) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("published")]
        public async Task<ActionResult<PaginationModel<BlogResponse>>> GetPublishedBlogs(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");
            var pagedBlogs = await _blogService.GetPublishedBlogsAsync(pageNumber, pageSize);
            return ResponseOk(pagedBlogs);
        }
    }
}

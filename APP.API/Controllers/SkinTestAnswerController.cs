using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.API.Controllers
{
    [Route("api/skin-test-answers")]
    [ApiController]
    public class SkinTestAnswerController : ControllerBase
    {
        private readonly ISkinTestAnswerService _skinTestAnswerService;

        public SkinTestAnswerController(ISkinTestAnswerService skinTestAnswerService)
        {
            _skinTestAnswerService = skinTestAnswerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkinTestAnswerResponse>>> GetAll()
        {
            var answers = await _skinTestAnswerService.GetAllAsync();
            return Ok(answers);
        }

        [HttpPost]
        public async Task<ActionResult<SkinTestAnswerResponse>> CreateSkinTestAnswer(SkinTestAnswerRequest request)
        {
            try
            {
                var answer = await _skinTestAnswerService.CreateSkinTestAnswerAsync(request);
                return CreatedAtAction(nameof(GetAll), new { id = answer?.AnswerId }, answer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

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

        [HttpGet("{id}")]
        public async Task<ActionResult<SkinTestAnswerResponse>> GetById(int id)
        {
            var answer = await _skinTestAnswerService.GetByIdAsync(id);
            return answer == null ? NotFound() : Ok(answer);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<SkinTestAnswerResponse>> GetByCustomerId(int customerId)
        {
            var answer = await _skinTestAnswerService.GetByCustomerId(customerId);
            return answer == null ? NotFound() : Ok(answer);
        }

        [HttpPost]
        public async Task<ActionResult<SkinTestAnswerResponse>> CreateSkinTestAnswer(SkinTestAnswerRequest request)
        {
            try
            {
                var answer = await _skinTestAnswerService.CreateSkinTestAnswerAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = answer?.AnswerId }, answer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkinTestAnswer(int id)
        {
            var result = await _skinTestAnswerService.DeleteSkinTestAnswerAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}

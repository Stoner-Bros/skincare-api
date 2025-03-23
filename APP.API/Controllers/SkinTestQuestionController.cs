using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.API.Controllers
{
    [Route("api/skin-test-questions")]
    [ApiController]
    public class SkinTestQuestionController : ControllerBase
    {
        private readonly ISkinTestQuestionService _skinTestQuestionService;

        public SkinTestQuestionController(ISkinTestQuestionService skinTestQuestionService)
        {
            _skinTestQuestionService = skinTestQuestionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkinTestQuestionResponse>>> GetAll()
        {
            var questions = await _skinTestQuestionService.GetAllAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkinTestQuestionResponse>> GetById(int id)
        {
            var question = await _skinTestQuestionService.GetByIDAsync(id);
            return question == null ? NotFound() : Ok(question);
        }

        [HttpGet("by-skin-test/{skinTestId}")]
        public async Task<ActionResult<IEnumerable<SkinTestQuestionResponse>>> GetBySkinTestId(int skinTestId)
        {
            var questions = await _skinTestQuestionService.GetBySkinTestIdAsync(skinTestId);
            return Ok(questions);
        }

        [HttpPost("{skinTestId}")]
        public async Task<ActionResult<SkinTestQuestionResponse>> Create(int skinTestId, SkinTestQuestionCreationRequest request)
        {
            var question = await _skinTestQuestionService.CreateAsync(skinTestId, request);
            return CreatedAtAction(nameof(GetById), new { id = question?.SkinTestQuestionId }, question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SkinTestQuestionUpdationRequest request)
        {
            var updatedQuestion = await _skinTestQuestionService.UpdateAsync(id, request);
            return updatedQuestion == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _skinTestQuestionService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

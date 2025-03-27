using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.API.Controllers
{
    [Route("api/skin-test-results")]
    [ApiController]
    public class SkinTestResultController : ControllerBase
    {
        private readonly ISkinTestResultService _skinTestResultService;

        public SkinTestResultController(ISkinTestResultService skinTestResultService)
        {
            _skinTestResultService = skinTestResultService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkinTestResultResponse>>> GetAll()
        {
            var results = await _skinTestResultService.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("{resultId}")]
        public async Task<ActionResult<SkinTestResultResponse>> GetByResultId(int resultId)
        {
            var result = await _skinTestResultService.GetByResultIdAsync(resultId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("answer/{id}")]
        public async Task<ActionResult<SkinTestResultResponse>> GetByAnswerId(int id)
        {
            var result = await _skinTestResultService.GetByAnswerIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("{skinTestAnswerId}")]
        public async Task<ActionResult<SkinTestResultResponse>> CreateBySkinTestAnswerId(int skinTestAnswerId, SkinTestResultRequest request)
        {
            try
            {
                var result = await _skinTestResultService.CreateBySkinTestAnswerIdAsync(skinTestAnswerId, request);
                return CreatedAtAction(nameof(GetByResultId), new { resultId = result?.ResultId }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{resultId}")]
        public async Task<IActionResult> Update(int resultId, SkinTestResultRequest request)
        {
            var updatedResult = await _skinTestResultService.UpdateAsync(resultId, request);
            return updatedResult == null ? NotFound() : NoContent();
        }

        [HttpDelete("{resultId}")]
        public async Task<IActionResult> Delete(int resultId)
        {
            var deleted = await _skinTestResultService.DeleteAsync(resultId);
            return deleted ? NoContent() : NotFound();
        }
    }
}

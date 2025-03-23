using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APP.API.Controllers
{
    [Route("api/skin-tests")]
    [ApiController]
    public class SkinTestController : ControllerBase
    {
        private readonly ISkinTestService _skinTestService;

        public SkinTestController(ISkinTestService skinTestService)
        {
            _skinTestService = skinTestService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkinTestResponse>>> GetAll()
        {
            var skinTests = await _skinTestService.GetAllAsync();
            return Ok(skinTests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkinTestResponse>> GetById(int id)
        {
            var skinTest = await _skinTestService.GetByIDAsync(id);
            return skinTest == null ? NotFound() : Ok(skinTest);
        }

        [HttpPost]
        public async Task<ActionResult<SkinTestResponse>> Create(SkinTestCreationRequest request)
        {
            var skinTest = await _skinTestService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = skinTest?.SkinTestId }, skinTest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SkinTestUpdationRequest request)
        {
            var updatedSkinTest = await _skinTestService.UpdateAsync(id, request);
            return updatedSkinTest == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _skinTestService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

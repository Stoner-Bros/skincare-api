using APP.API.Controllers.Helper;
using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/treatments")]
    [ApiController]
    public class TreatmentsController : ApiBaseController
    {
        private readonly ITreatmentService _treatmentService;
        public TreatmentsController(ITreatmentService treatmentService) => _treatmentService = treatmentService;

        [HttpGet]
        public async Task<ActionResult<PaginationModel<TreatmentResponse>>> GetTreatments(
                [FromQuery] int serviceId = 1,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");

            var pagedTreatments = await _treatmentService.GetAllAsync(serviceId, pageNumber, pageSize);

            return ResponseOk(pagedTreatments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TreatmentResponse>> GetTreatment(int id)
        {
            var treatment = await _treatmentService.GetByIDAsync(id);
            return treatment == null ? _respNotFound : ResponseOk(treatment);
        }

        [HttpPost]
        public async Task<ActionResult<TreatmentResponse>> CreateTreatment(TreatmentRequest request)
        {
            var treatment = await _treatmentService.CreateAsync(request);
            return CreatedAtAction(nameof(GetTreatment), new { id = treatment?.TreatmentId }, treatment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTreatment(int id, TreatmentRequest request)
        {
            if (!await _treatmentService.UpdateAsync(id, request)) return _respNotFound;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatment(int id)
        {
            if (!await _treatmentService.DeleteAsync(id)) return _respNotFound;
            return NoContent();
        }
    }
}

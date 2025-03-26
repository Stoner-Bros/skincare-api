using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APP.API.Controllers.Helper;
using APP.BLL.Implements;

namespace APP.API.Controllers
{
    [Route("api/consulting-forms")]
    [ApiController]
    public class ConsultingFormController : ApiBaseController
    {
        private readonly IConsultingFormService _consultingFormService;

        public ConsultingFormController(IConsultingFormService consultingFormService)
        {
            _consultingFormService = consultingFormService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationModel<object>>> GetBookings(
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");

            var forms = await _consultingFormService.GetAllAsync(pageNumber, pageSize);

            return ResponseOk(forms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBooking(int id)
        {
            var form = await _consultingFormService.GetByIDAsync(id);
            return form != null ? ResponseOk(form) : _respNotFound;
        }

        [HttpPost]
        public async Task<IActionResult> PostBooking(ConsultingFormCreationRequest request)
        {
            var success = await _consultingFormService.CreateAsync(request);

            return success ? ResponseOk() : _respBadRequest;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, ConsultingFormUpdationRequest request)
        {
            return await _consultingFormService.UpdateAsync(id, request) ? NoContent() : _respNotFound;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            return await _consultingFormService.DeleteAsync(id) ? NoContent() : _respNotFound;
        }
    }
}

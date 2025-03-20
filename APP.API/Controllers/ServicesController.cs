using APP.API.Controllers.Helper;
using APP.BLL.Implements;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ApiBaseController
    {
        private readonly IServiceService _serviceService;
        public ServicesController(IServiceService serviceService) => _serviceService = serviceService;

        [HttpGet]
        public async Task<ActionResult<PaginationModel<ServiceResponse>>> GetServices(
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10
            )
        {
            if (pageNumber < 1 || pageSize < 1)
                return ResponseNoData(400, "PageNumber and PageSize must greater than 0.");

            var pagedServices = await _serviceService.GetAllAsync(pageNumber, pageSize);

            return ResponseOk(pagedServices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetService(int id)
        {
            var service = await _serviceService.GetByIDAsync(id);
            return service == null ? _respNotFound : ResponseOk(service);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> CreateService(ServiceCreationRequest request)
        {
            var service = await _serviceService.CreateAsync(request);
            return CreatedAtAction(nameof(GetService), new { id = service?.ServiceId }, service);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, ServiceUpdationRequest request)
        {
            if (!await _serviceService.UpdateAsync(id, request)) return _respNotFound;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            if (!await _serviceService.DeleteAsync(id)) return _respNotFound;
            return NoContent();
        }
    }
}

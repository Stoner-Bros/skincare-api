using APP.API.Controllers.Helper;
using APP.BLL.Interfaces;
using APP.Entity.DTOs.Request;
using APP.Entity.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APP.API.Controllers
{
    [Route("api/overview")]
    [ApiController]
    public class OverviewController : ApiBaseController
    {
        private readonly IOverviewService _overviewService;

        public OverviewController(IOverviewService overviewService)
        {
            _overviewService = overviewService;
        }

        [HttpPost]
        public async Task<ActionResult<OverviewResponse>> GetOverview([FromBody] OverviewRequest request)
        {
            var overview = await _overviewService.GetOverviewAsync(request);
            return ResponseOk(overview);
        }
    }
}

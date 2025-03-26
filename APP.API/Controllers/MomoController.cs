using APP.API.Controllers.Helper;
using APP.BLL.Implements;
using APP.Entity.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace APP.API.Controllers
{
    [ApiController]
    [Route("api/momo")]
    public class MomoController : ApiBaseController
    {
        private readonly MomoService _momoService;
        public MomoController(MomoService momoService)
        {
            _momoService = momoService;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] MomoModel request)
        {
            var paymentUrl = await _momoService.CreatePaymentUrl(request.OrderId, request.Amount, request.OrderInfo, request.PaymentMethod);
            return Ok(paymentUrl);
        }

        [HttpGet("callback")]
        public IActionResult PaymentCallback([FromQuery] IQueryCollection query)
        {
            return Ok(query);
        }
    }

    public class MomoModel
    {
        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string OrderInfo { get; set; }
        public string PaymentMethod { get; set; }
    }
}

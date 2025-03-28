using APP.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APP.API.Controllers
{
    [ApiController]
    [Route("api/vnpay")]
    public class VnPayController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        public VnPayController(IConfiguration configuration, AppDbContext appContext)
        {
            _configuration = configuration;
            _context = appContext;
        }

        [HttpPost("create-payment")]
        public IActionResult CreatePayment([FromBody] VnPayModel request)
        {
            var vnp_Url = _configuration["VnPay:BaseUrl"];
            var vnp_TmnCode = _configuration["VnPay:TmnCode"];
            var vnp_HashSecret = _configuration["VnPay:HashSecret"];

            var vnp_Params = HttpUtility.ParseQueryString(string.Empty);
            vnp_Params["vnp_Version"] = "2.1.0";
            vnp_Params["vnp_Command"] = "pay";
            vnp_Params["vnp_TmnCode"] = vnp_TmnCode;
            vnp_Params["vnp_Amount"] = (int.Parse(request.Amount) * 100).ToString();
            vnp_Params["vnp_CurrCode"] = "VND";
            vnp_Params["vnp_TxnRef"] = request.OrderId;
            vnp_Params["vnp_OrderInfo"] = request.OrderInfo;
            vnp_Params["vnp_OrderType"] = "other";
            vnp_Params["vnp_Locale"] = "vn";
            vnp_Params["vnp_ReturnUrl"] = _configuration["VnPay:ReturnUrl"];
            vnp_Params["vnp_IpnUrl"] = _configuration["VnPay:IpnUrl"];
            vnp_Params["vnp_IpAddr"] = HttpContext.Connection.RemoteIpAddress?.ToString();
            vnp_Params["vnp_CreateDate"] = DateTime.Now.ToString("yyyyMMddHHmmss");

            string signData = string.Join("&", vnp_Params.AllKeys.OrderBy(k => k).Select(k => k + "=" + vnp_Params[k]));
            string vnp_SecureHash = ComputeHmacSha512(vnp_HashSecret, signData);
            vnp_Params["vnp_SecureHash"] = vnp_SecureHash;

            return Ok(vnp_Url + "?" + vnp_Params.ToString());
        }

        [HttpGet("callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] VnPayCallbackModel callback)
        {
            if (callback.vnp_ResponseCode == "00")
            {
                await _context.Settings.AddAsync(new Entity.Entities.Settings
                {
                    CentreName = "VnPay",
                    CentreAddress = "VnPay",
                    CentrePhoneNumber = "VnPay",
                    CentreEmail = "VnPay",
                    OpeningHours = new TimeSpan(8, 0, 0),
                    ClosingHours = new TimeSpan(17, 0, 0),
                    OpeningDays = "VnPay",
                    Description = "VnPay"
                });
                return Ok(new { Success = true, Message = "Thanh toán thành công!" });
            }
            return BadRequest(new { Success = false, Message = "Thanh toán thất bại!" });
        }

        [HttpPost("ipn")]
        public IActionResult PaymentIpn([FromQuery] VnPayCallbackModel callback)
        {
            if (callback.vnp_ResponseCode == "00")
            {
                // Xử lý đơn hàng thành công
                return Ok(new { RspCode = "00", Message = "Giao dịch thành công" });
            }
            return BadRequest(new { RspCode = "99", Message = "Giao dịch thất bại" });
        }

        private static string ComputeHmacSha512(string key, string data)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
        }
    }

    public class VnPayModel
    {
        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string OrderInfo { get; set; }
    }

    public class VnPayCallbackModel
    {
        public string vnp_TxnRef { get; set; }
        public string vnp_ResponseCode { get; set; }
    }
}

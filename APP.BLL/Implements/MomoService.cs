using APP.BLL.Interfaces;
using APP.BLL.UOW;
using APP.Entity.DTOs.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace APP.BLL.Implements
{
    public class MomoService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISkinTherapistScheduleService _skinTherapistScheduleService;
        public MomoService
            (IConfiguration configuration, IUnitOfWork unitOfWork, ISkinTherapistScheduleService skinTherapistScheduleService)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _skinTherapistScheduleService = skinTherapistScheduleService;
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(message))).Replace("-", "").ToLower();
        }

        public async Task<object> CreatePaymentUrl(string orderId, string amount, string orderInfo, string paymentMethod)
        {
            var momoConfig = _configuration.GetSection("MomoAPI");
            string requestType = paymentMethod switch
            {
                "momo" => "captureWallet",
                "atm" => "payWithATM",
                "cc" => "payWithCC",
                _ => "captureWallet"
            };

            var rawData = $"accessKey={momoConfig["AccessKey"]}&amount={amount}&extraData=&ipnUrl={momoConfig["IpnUrl"]}&orderId={orderId}&orderInfo={orderInfo}&partnerCode={momoConfig["PartnerCode"]}&redirectUrl={momoConfig["RedirectUrl"]}&requestId={orderId}&requestType={requestType}";
            var signature = ComputeHmacSha256(rawData, momoConfig["SecretKey"]);

            var requestData = new MomoPaymentRequest
            {
                PartnerCode = momoConfig["PartnerCode"],
                RequestType = requestType,
                IpnUrl = momoConfig["IpnUrl"],
                RedirectUrl = momoConfig["RedirectUrl"],
                OrderId = orderId,
                Amount = amount,
                OrderInfo = orderInfo,
                RequestId = orderId,
                Lang = "vi",
                Signature = signature,
                ExtraData = ""
            };

            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync(momoConfig["MomoApiUrl"], requestData);
            var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();
            return responseData;
        }

        public async Task<object> PaymentExecuteAsync(JsonElement requestBody)
        {
            if (!requestBody.TryGetProperty("orderId", out var orderIdProperty) ||
                !requestBody.TryGetProperty("resultCode", out var resultCodeProperty))
            {
                return new { Success = false, Message = "Thiếu dữ liệu quan trọng." };
            }

            string orderId = orderIdProperty.GetString();
            int resultCode = resultCodeProperty.GetInt32();

            if (!orderId.Contains("SS"))
            {
                return new { Success = false, Message = "orderId không hợp lệ." };
            }

            string bookingIdStr = orderId.Split("SS")[0];

            if (!int.TryParse(bookingIdStr, out int bookingId))
            {
                return new { Success = false, Message = "Không thể chuyển đổi BookingId thành số." };
            }

            var booking = await _unitOfWork.Bookings.GetQueryable()
                .Include(b => b.Payment)
                .Include(b => b.SkinTherapist)
                .Include(b => b.BookingTimeSlots)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
            {
                return new { Success = false, Message = "Không tìm thấy Booking." };
            }

            if (resultCode != 0) // 0 = Giao dịch thành công
            {
                if (booking.SkinTherapistId != null && booking.SkinTherapist != null && booking.BookingTimeSlots.Count != 0)
                {
                    await _skinTherapistScheduleService.ReverseScheduleAsync(
                        booking.SkinTherapist.AccountId,
                        booking.BookingTimeSlots.First().Date,
                        [.. booking.BookingTimeSlots.Select(b => b.TimeSlotId)]
                    );
                }
                return new { Success = false, Message = "Giao dịch thất bại", Code = resultCode };

            }

            booking.Status = "Paid";
            booking.Payment.PaymentStatus = "Paid";
            await _unitOfWork.SaveAsync();

            return new { Success = true, BookingId = bookingId };
        }
    }

}

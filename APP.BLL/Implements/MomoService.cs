﻿using APP.Entity.DTOs.Request;
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
        public MomoService(IConfiguration configuration)
        {
            _configuration = configuration;
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
    }

}

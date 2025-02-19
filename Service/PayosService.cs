using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Data.Models;
using Microsoft.Extensions.Configuration;

namespace Service
{
    public class PayosService
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly PayosConfig _config;

        public PayosService(IPaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _httpClient = new HttpClient();
            
            // Đọc config một cách đơn giản hơn
            _config = new PayosConfig
            {
                ClientId = configuration["Payos:ClientId"],
                ApiKey = configuration["Payos:ApiKey"],
                ChecksumKey = configuration["Payos:ChecksumKey"],
                ApiUrl = configuration["Payos:ApiUrl"],
                ReturnUrl = configuration["Payos:ReturnUrl"],
                CancelUrl = configuration["Payos:CancelUrl"],
                WebhookUrl = configuration["Payos:WebhookUrl"]
            };
        }

        public async Task<PaymentResponse> CreatePaymentRequest(int orderId, decimal amount, string description)
        {
            var orderCode = $"ORDER_{orderId}_{DateTime.Now.Ticks}";
            
            var paymentData = new
            {
                orderCode = orderCode,
                amount = (long)amount,
                description = description,
                cancelUrl = _config.CancelUrl,
                returnUrl = _config.ReturnUrl,
                signature = CreateSignature(orderCode, amount, description)
            };

            var request = new HttpRequestMessage(HttpMethod.Post, _config.ApiUrl)
            {
                Content = new StringContent(JsonSerializer.Serialize(paymentData), Encoding.UTF8, "application/json")
            };

            request.Headers.Add("x-client-id", _config.ClientId);
            request.Headers.Add("x-api-key", _config.ApiKey);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"PayOS request failed: {content}");
            }

            // Tạo payment record với trạng thái PENDING
            var payment = new Payment
            {
                OrderId = orderId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                PaymentStatus = "PENDING"
            };

            await _paymentService.AddPaymentAsync(payment);

            return JsonSerializer.Deserialize<PaymentResponse>(content);
        }

        public async Task HandleWebhook(PayosWebhookPayload payload, string signature)
        {
            try
            {
                if (!VerifyWebhookSignature(payload, signature))
                {
                    throw new Exception("Invalid webhook signature");
                }

                // Extract order info
                var orderCode = payload.Data.OrderCode;
                var amount = payload.Data.Amount;
                var status = payload.Data.Code == "00" ? "PAID" : "FAILED";

                // Update payment in database
                var payment = await _paymentService.GetPaymentByOrderIdAsync(int.Parse(orderCode));
                if (payment != null)
                {
                    payment.PaymentStatus = status;
                    payment.PaymentDate = DateTime.Parse(payload.Data.TransactionDateTime);
                    await _paymentService.UpdatePaymentAsync(payment);

                    // Add payment history
                    var history = new PaymentHistory
                    {
                        PaymentId = payment.PaymentId,
                        PaymentDate = DateTime.Parse(payload.Data.TransactionDateTime),
                        Amount = amount,
                        PaymentStatus = status
                    };
                    await _paymentService.AddPaymentHistoryAsync(history);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to process webhook: {ex.Message}");
            }
        }

        private string CreateSignature(string orderCode, decimal amount, string description)
        {
            var signData = $"{orderCode}|{amount}|{description}|{_config.CancelUrl}|{_config.ReturnUrl}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_config.ChecksumKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signData));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private bool VerifyWebhookSignature(PayosWebhookPayload payload, string signature)
        {
            // Verify using payload.Signature instead of header signature
            return payload.Signature == signature;
        }
    }

    public class PayosConfig
    {
        public string ClientId { get; set; }
        public string ApiKey { get; set; }
        public string ChecksumKey { get; set; }
        public string ApiUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
        public string WebhookUrl { get; set; }
    }

    public class PaymentResponse
    {
        public string CheckoutUrl { get; set; }
        public string QrCode { get; set; }
        // Thêm các properties khác nếu cần
    }

    public class PayosWebhookPayload
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public PayosWebhookData Data { get; set; }
        public string Signature { get; set; }
    }

    public class PayosWebhookData
    {
        public string OrderCode { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string AccountNumber { get; set; }
        public string Reference { get; set; }
        public string TransactionDateTime { get; set; }
        public string PaymentLinkId { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public string CounterAccountBankId { get; set; }
        public string CounterAccountBankName { get; set; }
        public string CounterAccountName { get; set; }
        public string CounterAccountNumber { get; set; }
        public string VirtualAccountName { get; set; }
        public string VirtualAccountNumber { get; set; }
        public string Currency { get; set; }
    }
} 
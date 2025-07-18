using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FurEverCarePlatform.Application.Services
{
    public class VNPayService
    {
        private readonly VNPaySettings _vnPaySettings;

        public VNPayService(IOptions<VNPaySettings> vnPaySettings, IConfiguration configuration)
        {
            _vnPaySettings = vnPaySettings.Value;
        }

        public Task<string> RequestVNPay(string orderCode, float price, Guid? prescriptionId)
        {
            try
            {
                var pay = new VnPayLibrary();

                string returnUrl = _vnPaySettings.ReturnUrlUser;
                pay.AddRequestData("vnp_Version", _vnPaySettings.Version ?? "2.1.0");
                pay.AddRequestData("vnp_Command", _vnPaySettings.Command ?? "pay");
                pay.AddRequestData("vnp_TmnCode", _vnPaySettings.TmnCode);
                pay.AddRequestData("vnp_Amount", ((int)(price * 100)).ToString());
                pay.AddRequestData("vnp_CreateDate", DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", _vnPaySettings.CurrCode ?? "VND");
                pay.AddRequestData("vnp_IpAddr", "127.0.0.1");
                pay.AddRequestData("vnp_Locale", _vnPaySettings.Locale ?? "vn");
                pay.AddRequestData("vnp_OrderInfo", $"Thanh toan cho don hang: {orderCode}");
                pay.AddRequestData("vnp_OrderType", "250000");
                pay.AddRequestData("vnp_TxnRef", orderCode);
                pay.AddRequestData("vnp_ReturnUrl", returnUrl);

                var paymentUrl = pay.CreateRequestUrl(
                    _vnPaySettings.BaseUrl,
                    _vnPaySettings.HashSecret
                );

                return Task.FromResult(
                    !string.IsNullOrEmpty(paymentUrl)
                        ? paymentUrl
                        : "VNPay không trả về payment URL."
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tạo yêu cầu VNPay: {ex.Message}");
            }
        }

        public bool ReturnFromVNPay(VNPayModel vnPayResponse)
        {
            try
            {
                if (vnPayResponse == null)
                {
                    throw new Exception("VNPay response is null");
                }

                var vnpay = new VnPayLibrary();

                // Add all response data to VnPayLibrary for signature validation
                foreach (PropertyInfo prop in vnPayResponse.GetType().GetProperties())
                {
                    string name = prop.Name;
                    object value = prop.GetValue(vnPayResponse, null);
                    string valueStr = value?.ToString() ?? string.Empty;
                    vnpay.AddResponseData(name, valueStr);
                }

                // Validate signature
                bool validateSignature = vnpay.ValidateSignature(
                    vnPayResponse.vnp_SecureHash,
                    _vnPaySettings.HashSecret
                );

                if (!validateSignature)
                {
                    throw new Exception("Invalid VNPay signature!");
                }

                // Check if payment is successful
                if (vnPayResponse.vnp_ResponseCode == "00")
                {
                    return true;
                }
                else
                {
                    string errorMessage = GetVNPayErrorMessage(vnPayResponse.vnp_ResponseCode);
                    throw new Exception($"Thanh toán thất bại: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xử lý phản hồi từ VNPay: {ex.Message}");
            }
        }

        private string GetVNPayErrorMessage(string responseCode)
        {
            return responseCode switch
            {
                "00" => "Giao dịch thành công",
                "07" =>
                    "Trừ tiền thành công. Giao dịch bị nghi ngờ (liên quan tới lừa đảo, giao dịch bất thường).",
                "09" =>
                    "Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng chưa đăng ký dịch vụ InternetBanking tại ngân hàng.",
                "10" =>
                    "Giao dịch không thành công do: Khách hàng xác thực thông tin thẻ/tài khoản không đúng quá 3 lần",
                "11" =>
                    "Giao dịch không thành công do: Đã hết hạn chờ thanh toán. Xin quý khách vui lòng thực hiện lại giao dịch.",
                "12" => "Giao dịch không thành công do: Thẻ/Tài khoản của khách hàng bị khóa.",
                "13" =>
                    "Giao dịch không thành công do Quý khách nhập sai mật khẩu xác thực giao dịch (OTP).",
                "24" => "Giao dịch không thành công do: Khách hàng hủy giao dịch",
                "51" =>
                    "Giao dịch không thành công do: Tài khoản của quý khách không đủ số dư để thực hiện giao dịch.",
                "65" =>
                    "Giao dịch không thành công do: Tài khoản của Quý khách đã vượt quá hạn mức giao dịch trong ngày.",
                "75" => "Ngân hàng thanh toán đang bảo trì.",
                "79" =>
                    "Giao dịch không thành công do: KH nhập sai mật khẩu thanh toán quá số lần quy định.",
                "99" => "Các lỗi khác (lỗi còn lại, không có trong danh sách mã lỗi đã liệt kê)",
                _ => $"Lỗi không xác định: {responseCode}",
            };
        }

        #region VnPayLibrary - Private Implementation
        private class VnPayLibrary
        {
            public const string VERSION = "2.1.0";
            private SortedList<string, string> _requestData = new SortedList<string, string>(
                new VnPayCompare()
            );
            private SortedList<string, string> _responseData = new SortedList<string, string>(
                new VnPayCompare()
            );

            public void AddRequestData(string key, string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _requestData.Add(key, value);
                }
            }

            public void AddResponseData(string key, string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _responseData.Add(key, value);
                }
            }

            public string GetResponseData(string key)
            {
                string retValue;
                if (_responseData.TryGetValue(key, out retValue))
                {
                    return retValue;
                }
                else
                {
                    return string.Empty;
                }
            }

            public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
            {
                StringBuilder data = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in _requestData)
                {
                    if (!string.IsNullOrEmpty(kv.Value))
                    {
                        data.Append(
                            WebUtility.UrlEncode(kv.Key)
                                + "="
                                + WebUtility.UrlEncode(kv.Value)
                                + "&"
                        );
                    }
                }
                string queryString = data.ToString();
                baseUrl += "?" + queryString;
                string signData = queryString;
                if (signData.Length > 0)
                {
                    signData = signData.Remove(data.Length - 1, 1);
                }
                string vnp_SecureHash = HmacSHA512(vnp_HashSecret, signData);
                baseUrl += "vnp_SecureHash=" + vnp_SecureHash;
                return baseUrl;
            }

            public bool ValidateSignature(string inputHash, string secretKey)
            {
                string rspRaw = GetResponseData();
                string myChecksum = HmacSHA512(secretKey, rspRaw);
                return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
            }

            private string GetResponseData()
            {
                StringBuilder data = new StringBuilder();
                if (_responseData.ContainsKey("vnp_SecureHashType"))
                {
                    _responseData.Remove("vnp_SecureHashType");
                }
                if (_responseData.ContainsKey("vnp_SecureHash"))
                {
                    _responseData.Remove("vnp_SecureHash");
                }
                foreach (KeyValuePair<string, string> kv in _responseData)
                {
                    if (!string.IsNullOrEmpty(kv.Value))
                    {
                        data.Append(
                            WebUtility.UrlEncode(kv.Key)
                                + "="
                                + WebUtility.UrlEncode(kv.Value)
                                + "&"
                        );
                    }
                }
                if (data.Length > 0)
                {
                    data.Remove(data.Length - 1, 1);
                }
                return data.ToString();
            }

            private string HmacSHA512(string key, string inputData)
            {
                var hash = new StringBuilder();
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
                using (var hmac = new HMACSHA512(keyBytes))
                {
                    byte[] hashValue = hmac.ComputeHash(inputBytes);
                    foreach (var theByte in hashValue)
                    {
                        hash.Append(theByte.ToString("x2"));
                    }
                }
                return hash.ToString();
            }
        }

        private class VnPayCompare : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x == y)
                    return 0;
                if (x == null)
                    return -1;
                if (y == null)
                    return 1;
                var vnpCompare = CompareInfo.GetCompareInfo("en-US");
                return vnpCompare.Compare(x, y, CompareOptions.Ordinal);
            }
        }
        #endregion
    }
}

public class VNPayModel
{
    public string vnp_TmnCode { get; set; } = string.Empty;
    public string vnp_BankCode { get; set; } = string.Empty;
    public string? vnp_BankTranNo { get; set; } = string.Empty;
    public string vnp_CardType { get; set; } = string.Empty;
    public string vnp_OrderInfo { get; set; } = string.Empty;
    public string vnp_TransactionNo { get; set; } = string.Empty;
    public string vnp_TransactionStatus { get; set; } = string.Empty;
    public string vnp_TxnRef { get; set; } = string.Empty;
    public string? vnp_SecureHashType { get; set; } = string.Empty;
    public string vnp_SecureHash { get; set; } = string.Empty;
    public int? vnp_Amount { get; set; }
    public string? vnp_ResponseCode { get; set; }
    public string vnp_PayDate { get; set; } = string.Empty;
}

public class VNPaySettings
{
    public string Version { get; set; }
    public string Command { get; set; }
    public string TmnCode { get; set; }
    public string CurrCode { get; set; }
    public string Locale { get; set; }
    public string ReturnUrlUser { get; set; }
    public string ReturnUrlCounter { get; set; }
    public string ReturnUrlCounterCreate { get; set; }
    public string ReturnUrlPrescription { get; set; }
    public string ReturnUrlApp { get; set; }
    public string BaseUrl { get; set; }
    public string HashSecret { get; set; }
}

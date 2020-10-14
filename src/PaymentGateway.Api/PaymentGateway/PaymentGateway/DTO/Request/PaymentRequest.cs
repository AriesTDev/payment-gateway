using Newtonsoft.Json;

namespace PaymentGateway.DTO.Request
{
    public class PaymentRequest
    {
        [JsonProperty("txamt")]
        public int  Amount { get; set; }

        [JsonProperty("txcurrcd")]
        public string Currency { get; set; }

        [JsonProperty("pay_type")]
        public string PaymentType { get; set; }

        [JsonProperty("out_trade_no")]
        public string TransactionNumber { get; set; }

        [JsonProperty("txdtm")]
        public string TransactionDateTime { get; set; }

        [JsonProperty("goods_name")]
        public string GoodsName { get; set; }

        [JsonProperty("mchid")]
        public string MerchantId { get; set; }
    }

    public class MpmPaymentRequest : PaymentRequest
    {
        //[JsonProperty("expired_time")]
        //public string  ExpiredTime { get; set; }//optional
    }

    public class CpmPaymentRequest : PaymentRequest
    {
        [JsonProperty("auth_code")]
        public string  AuthCode { get; set; }
    }

    public class AlipayOnlinePayPaymentRequest : PaymentRequest
    {
        [JsonProperty("return_url")]
        public string ReturnUrl { get; set; }
    }

    public class WechatOnlinePayPaymentRequest : PaymentRequest
    {
    }
}

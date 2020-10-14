using Newtonsoft.Json;

namespace PaymentGateway.DTO.Response
{
    public class PaymentResponse
    {
        [JsonProperty("pay_type")]
        public string PaymentType { get; set; }

        [JsonProperty("sysdtm")]
        public string SystemTransactionDatetime { get; set; }
        [JsonProperty("txdtm")]
        public string RequestTransactionDatetime { get; set; }

        [JsonProperty("resperr")]
        public string ResponseError { get; set; }

        [JsonProperty("txamt")]
        public int Amount { get; set; }

        [JsonProperty("respmsg")]
        public string ResponseMessage { get; set; }

        [JsonProperty("out_trade_no")]
        public string TransactionNumber { get; set; }
        [JsonProperty("syssn")]
        public string QFPayTransactionNumber { get; set; }

        [JsonProperty("respcd")]
        public string ResponseCode { get; set; }

        [JsonProperty("qrcode")]
        public string QrCode { get; set; }

        [JsonProperty("pay_url")]
        public string PayUrl { get; set; }

        [JsonProperty("cardcd")]
        public string CardNumber { get; set; }

        [JsonProperty("udid")]
        public string DeviceId { get; set; }

        [JsonProperty("chnlsn")]
        public string ChannelCode { get; set; }
    }
}

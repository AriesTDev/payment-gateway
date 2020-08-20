using Newtonsoft.Json;

namespace Aitea.PaymentGateway.DTO.Request
{
    public class InquirePaymentRequest
    {
        [JsonProperty("mchid",
            NullValueHandling = NullValueHandling.Ignore)]
        public string MerchantId { get; set; }

        [JsonProperty("syssn",
            NullValueHandling = NullValueHandling.Ignore)]
        public string QFPayTransactionNumber { get; set; }

        [JsonProperty("out_trade_no",
            NullValueHandling = NullValueHandling.Ignore)]
        public string TransactionNumber { get; set; }

        [JsonProperty("pay_type",
            NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentType { get; set; }

        [JsonProperty("respcd",
            NullValueHandling = NullValueHandling.Ignore)]
        public string TransactionReturnCode { get; set; }

        [JsonProperty("start_time",
            NullValueHandling = NullValueHandling.Ignore)]
        public string StartTime { get; set; }

        [JsonProperty("end_time",
            NullValueHandling = NullValueHandling.Ignore)]
        public string EndTime { get; set; }

        [JsonProperty("txzone",
            NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; }

        [JsonProperty("page",
            NullValueHandling = NullValueHandling.Ignore)]
        public int? Page { get; set; }

        [JsonProperty("page_size",
            NullValueHandling = NullValueHandling.Ignore)]
        public int? PageSize { get; set; }

    }

}

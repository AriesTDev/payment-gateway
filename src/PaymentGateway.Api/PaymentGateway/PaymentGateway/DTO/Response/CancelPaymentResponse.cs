using Newtonsoft.Json;

namespace PaymentGateway.DTO.Response
{
    public class CancelPaymentResponse
    {
        [JsonProperty("syssn")]
        public string QFPayTransactionNumber { get; set; }

        [JsonProperty("orig_syssn")]
        public string PrevQFPayTransactionNumber { get; set; }

        [JsonProperty("txamt")]
        public int RefundAmount { get; set; }

        [JsonProperty("sysdtm")]
        public string SystemTransactionDatetime { get; set; }
        
        [JsonProperty("respcd")]
        public string ReturnCode { get; set; }

        [JsonProperty("resperr")]
        public string ResponseError { get; set; }

        [JsonProperty("respmsg")]
        public string ResponseMessage { get; set; }
    }
}

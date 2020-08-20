using Newtonsoft.Json;

namespace Aitea.PaymentGateway.DTO.Request
{
    public class CancelPaymentRequest
    {
        [JsonProperty("syssn")]
        public string QFPayTransactionNumber { get; set; }

        [JsonProperty("out_trade_no")]
        public string TransactionNumber { get; set; }
        
        [JsonProperty("txamt")]
        public int  RefundAmount { get; set; }

        [JsonProperty("txdtm")]
        public string TransactionDateTime { get; set; }

        [JsonProperty("mchid")]
        public string MerchantId { get; set; }
        
    }

}

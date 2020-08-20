using Newtonsoft.Json;

namespace Aitea.PaymentGateway.DTO.Response
{
    public class InquirePaymentResponse
    {
        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("resperr")]
        public string ResponseError { get; set; }

        [JsonProperty("page_size")]
        public int? PageSize { get; set; }

        [JsonProperty("respcd")]
        public string ReturnCode { get; set; }

        [JsonProperty("data")]
        public object Result { get; set; }
        
        [JsonProperty("syssn")]
        public string QFPayTransactionNumber { get; set; }

        [JsonProperty("out_trade_no")]
        public string TransactionNumber { get; set; }

        [JsonProperty("goods_name")]
        public string GoodsName { get; set; }

        [JsonProperty("txcurrcd")]
        public string Currency { get; set; }

        [JsonProperty("origssn")]
        public string OrigTransactionNumber { get; set; }

        [JsonProperty("pay_type")]
        public string PaymentType { get; set; }

        [JsonProperty("order_type")]
        public string OrderType { get; set; }

        [JsonProperty("txdtm")]
        public string TransactionDatetime { get; set; }

        [JsonProperty("txamt")]
        public int TransactionAmount { get; set; }

        [JsonProperty("sysdtm")]
        public string SystemTransactionDatetime { get; set; }

        [JsonProperty("cancel")]
        public string Cancel { get; set; }

        [JsonProperty("errmsg")]
        public string StatusMessage { get; set; }

    }
}

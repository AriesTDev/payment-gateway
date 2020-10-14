using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PaymentGateway.DTO.Request
{
    public class NotifyPaymentRequest
    {
        [JsonProperty("status")]
        public string  Status { get; set; }
        [JsonProperty("pay_type")]
        public string PayType { get; set; }
        [JsonProperty("sysdtm")]
        public string SystemDatetime { get; set; }
        [JsonProperty("paydtm")]
        public string PaymentDatetime { get; set; }
        [JsonProperty("txcurrcd")]
        public string Currency { get; set; }
        [JsonProperty("txdtm")]
        public string TransactionDatetime { get; set; }
        [JsonProperty("txamt")]
        public string Amount { get; set; }
        [JsonProperty("out_trade_no")]
        public string TransactionNumber { get; set; }
        [JsonProperty("syssn")]
        public string QfPayTransactionNumber { get; set; }
        [JsonProperty("cancel")]
        public string Cancel { get; set; }
        [JsonProperty("respcd")]
        public string  ResponseCode { get; set; }
        [JsonProperty("notify_type")]
        public string NotifyType { get; set; }
        [JsonProperty("mchid")]
        public string MerchantId { get; set; }
        [JsonProperty("goods_name")]
        public string GoodsName { get; set; }
        [JsonProperty("exchange_rate")]
        public string ExchangeRate { get; set; }
        [JsonProperty("chnlsn2")]
        public string AdditionalTransactionNumber { get; set; }
        [JsonProperty("cash_fee")]
        public string CashFee { get; set; }
        [JsonProperty("cash_fee_type")]
        public string CashFeeType { get; set; }
        [JsonProperty("goods_info")]
        public string GoodsInfo { get; set; }
        [JsonProperty("chnlsn")]
        public string ChannelTransactionNumber { get; set; }
        [JsonProperty("cardcd")]
        public string CardNumber { get; set; }
    }
}

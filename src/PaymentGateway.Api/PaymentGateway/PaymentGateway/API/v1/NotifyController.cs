using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Contracts;
using PaymentGateway.Core.Constant;
using PaymentGateway.Core.Extensions;
using PaymentGateway.DTO.Request;
using PaymentGateway.DTO.Response;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace PaymentGateway.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IQfPayApiConnect _qfPayApiConnect;
        private readonly IMapper _mapper;
        public NotifyController(IQfPayApiConnect qfPayApiConnect, ILogger<PaymentController> logger, IMapper mapper)
        {
            _qfPayApiConnect = qfPayApiConnect;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromHeader] object headers, [FromBody] NotifyPaymentRequest notifyRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            var re = Request;
            var headers2 = re.Headers;
            string token = string.Empty;
            StringValues x = default(StringValues);
            if (headers2.ContainsKey("X-QF-SIGN"))
            {
                var m = headers2.TryGetValue("X-QF-SIGN", out x);
            }


            //string appcode = "123456";
            //string key = "123456";
            //string mchid = "eqqmYMn0Zj6pncw5ZDxjgMqbzV";
            //var txdtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //var goodsname = paymentRequest.GoodsName;
            //var transactionnumber = paymentRequest.TransactionNumber;
            //var txcurrcd = paymentRequest.Currency;
            //var txamt = paymentRequest.Amount;

            //var paytype = "alipay".GetQFPayPaymentTypeCode();
            //var request = new Dictionary<string, string>
            //{
            //    { "PaymentType", paytype },
            //    { "Amount", Convert.ToInt32(txamt).ToString()},
            //    { "Currency", txcurrcd},
            //    { "GoodsName", goodsname},
            //    { "MerchantId", mchid},
            //    { "TransactionDateTime", txdtm},
            //    { "TransactionNumber", transactionnumber}
            //};

            //signature generation
            //var toSign = $"goods_name={goodsname}&mchid={mchid}&out_trade_no={transactionnumber}" +
            //             $"&pay_type={paytype}&txamt={txamt}&txcurrcd={txcurrcd}&txdtm={txdtm}{key}";
            //_logger.Log(LogLevel.Information, $"Hashing input: {toSign.HideKey(key)}, key: {key.HideKey(key)}");
            //var sha256Hash = toSign.ToSha256Hash();

            ////header authentication
            //var headers = new Dictionary<string, string>
            //{
            //    { "X-QF-APPCODE", appcode },
            //    { "X-QF-SIGN" , sha256Hash }
            //};
            //return new ApiResponse(await _qfPayApiConnect.PostFormDataAsync<PaymentResponse>("/trade/v1/payment", request, headers));


            return await  Task.FromResult(new ApiResponse("SUCCESS",notifyRequest));
        }
    }
}

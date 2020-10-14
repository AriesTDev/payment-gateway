using System;
using System.Text.Json;
using PaymentGateway.Contracts;
using PaymentGateway.DTO.Request;
using PaymentGateway.DTO.Response;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PaymentGateway.Core.Constant;
using PaymentGateway.Core.Extensions;
using AutoMapper;
using static Microsoft.AspNetCore.Http.StatusCodes;
using System.Collections.Generic;
using PaymentGateway.DTO.RequestExtensions;
using PaymentGateway.Infrastructure.Configs;
using PaymentGateway.Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Utilities;
using Newtonsoft.Json;

namespace PaymentGateway.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IQfPayApiConnect _qfPayApiConnect;
        private readonly IMapper _mapper;
        private readonly QfPayConfiguration _qfPayConfiguration;

        public PaymentController(IQfPayApiConnect qfPayApiConnect, ILogger<PaymentController> logger, IMapper mapper, IOptionsSnapshot<QfPayConfiguration> config)
        {
            _qfPayApiConnect = qfPayApiConnect;
            _logger = logger;
            _mapper = mapper;
            _qfPayConfiguration = config.Value;
        }

        [Route("{id:long}")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        public async Task<ApiResponse> Get(long id)
        {
            string appcode = "123456";
            string key = "123456";
            string mchid = "eqqmYMn0Zj6pncw5ZDxjgMqbzV";
            var txdtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


            var test = "wechat".GetQFPayPaymentTypeCode();
            var returnVal = new PaymentRequest
            {
                PaymentType = test,
                Amount = 1100,
                Currency = "SGD"
            };
            return new ApiResponse("Successful payment request", returnVal);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Post([FromBody] ClientPaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            //NOTE: start of transactionNumber = TEST1001 , need to revert in the future

            var requestMap = _mapper.Map(paymentRequest, paymentRequest.GetPaymentType("http://localhost:44321/callback"));

            requestMap.TransactionDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            requestMap.MerchantId = _qfPayConfiguration.MerchantId;
            
            //signature generation
            var toSign = $"{requestMap.ToDictionary().GetDataString()}{_qfPayConfiguration.ApiKey}";
            _logger.Log(LogLevel.Information, $"Hashing input: {toSign.HideKey(_qfPayConfiguration.ApiKey)}, key: {_qfPayConfiguration.ApiKey.HideKey(_qfPayConfiguration.ApiKey)}");
            var sha256Hash = toSign.ToSha256Hash();

            //header authentication
            var headers = new Dictionary<string, string>
            {
                {"X-QF-APPCODE", _qfPayConfiguration.AppCode },
                {"X-QF-SIGN" , sha256Hash },
                {"X-QF-SIGNTYPE", "SHA256"}
            };

            ////temporary disable in order to see actual response from QFPay
            //request to qfpay api
            var requestToQfPayResponse =
                await _qfPayApiConnect.PostFormDataAsync<PaymentResponse>("/trade/v1/payment",
                    requestMap.ToDictionary(), headers).ConfigureAwait(false);
            _logger.Log(LogLevel.Information,"Response from QFPayApi Request Payment",requestToQfPayResponse);

            ////map result for client return 
            //var responseToClient = _mapper.Map<PaymentResponseToClient>(requestToQfPayResponse);
            //_logger.Log(LogLevel.Information,"Return to Client Response",responseToClient);

            return new ApiResponse(requestToQfPayResponse);
        }

        [HttpPost("Cancel")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Refund([FromBody] ClientCancelPaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            var requestMap = _mapper.Map(paymentRequest, new CancelPaymentRequest());

            requestMap.TransactionDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            requestMap.MerchantId = _qfPayConfiguration.MerchantId;


            //signature generation
            var toSign = $"{requestMap.ToDictionary().GetDataString()}{_qfPayConfiguration.ApiKey}";
            _logger.Log(LogLevel.Information, $"Hashing input: {toSign.HideKey(_qfPayConfiguration.ApiKey)}, key: {_qfPayConfiguration.ApiKey.HideKey(_qfPayConfiguration.ApiKey)}");
            var sha256Hash = toSign.ToSha256Hash();

            //header authentication
            var headers = new Dictionary<string, string>
            {
                {"X-QF-APPCODE", _qfPayConfiguration.AppCode },
                {"X-QF-SIGN" , sha256Hash },
                {"X-QF-SIGNTYPE", "SHA256"}
            };

            //request to qfpay api
            var requestToQfPayResponse =
                await _qfPayApiConnect.PostFormDataAsync<CancelPaymentResponse>("/trade/v1/refund",
                    requestMap.ToDictionary(), headers).ConfigureAwait(false);
            _logger.Log(LogLevel.Information, "Response from QFPayApi Cancel Payment", requestToQfPayResponse);

            ////temporary disable in order to see actual response from QFPay
            ////map result for client return 
            //var responseToClient = _mapper.Map<PaymentResponseToClient>(requestToQfPayResponse);
            //_logger.Log(LogLevel.Information, "Return to Client Response", responseToClient);

            return new ApiResponse(requestToQfPayResponse);
        }


        [HttpPost("Query")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Inquiry([FromBody] ClientInquiryRequest inquiryRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            var requestMap = _mapper.Map(inquiryRequest, new InquirePaymentRequest());
            requestMap.MerchantId = _qfPayConfiguration.MerchantId;

            //signature generation
            var toSign = $"{requestMap.ToDictionary().GetDataString()}{_qfPayConfiguration.ApiKey}";
            _logger.Log(LogLevel.Information, $"Hashing input: {toSign.HideKey(_qfPayConfiguration.ApiKey)}, key: {_qfPayConfiguration.ApiKey.HideKey(_qfPayConfiguration.ApiKey)}");
            var sha256Hash = toSign.ToSha256Hash();

            //header authentication
            var headers = new Dictionary<string, string>
            {
                {"X-QF-APPCODE", _qfPayConfiguration.AppCode },
                {"X-QF-SIGN" , sha256Hash },
                {"X-QF-SIGNTYPE", "SHA256"}
            };

            //request to qfpay api
            var requestToQfPayResponse =
                await _qfPayApiConnect.PostFormDataAsync<InquirePaymentResponse>("/trade/v1/query",
                    requestMap.ToDictionary(), headers).ConfigureAwait(false);
            _logger.Log(LogLevel.Information, "Response from QFPayApi Inquire Payment", requestToQfPayResponse);


            ////temporary disable in order to see actual response from QFPay
            ////map result for client return 
            //var responseToClient = _mapper.Map<InquirePaymentResponseToClient>(requestToQfPayResponse);
            //_logger.Log(LogLevel.Information, "Return to Client Response", responseToClient);

            return new ApiResponse(requestToQfPayResponse);
        }
    }
}

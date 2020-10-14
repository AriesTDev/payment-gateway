using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaymentGateway.Contracts;
using PaymentGateway.DTO.Request;
using PaymentGateway.DTO.RequestExtensions;
using PaymentGateway.Infrastructure.Configs;
using PaymentGateway.Infrastructure.Handlers;
using PaymentGateway.Core.Extensions;
using PaymentGateway.DTO.Response;

namespace Deposit.PG.Handler.Handlers
{
    [Handler("QFPay")]
    public class QFPayHandler : IHandler
    {
        private readonly ILogger<QFPayHandler> _logger;
        private readonly IQfPayApiConnect _qfPayApiConnect;
        private readonly IMapper _mapper;
        private readonly QfPayConfiguration _qfPayConfiguration;

        public QFPayHandler(IQfPayApiConnect qfPayApiConnect, 
            ILogger<QFPayHandler> logger, 
            IMapper mapper, 
            IOptionsSnapshot<QfPayConfiguration> config)
        {
            _qfPayApiConnect = qfPayApiConnect;
            _logger = logger;
            _mapper = mapper;
            _qfPayConfiguration = config.Value;
        }

        public async Task<ApiResponse> Inquiry(ClientInquiryRequest inquiryRequest)
        {
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

        public async Task<ApiResponse> Payment(ClientPaymentRequest paymentRequest)
        {
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
            _logger.Log(LogLevel.Information, "Response from QFPayApi Request Payment", requestToQfPayResponse);

            ////map result for client return 
            //var responseToClient = _mapper.Map<PaymentResponseToClient>(requestToQfPayResponse);
            //_logger.Log(LogLevel.Information,"Return to Client Response",responseToClient);

            return new ApiResponse(requestToQfPayResponse);
        }

        public async Task<ApiResponse> Refund(ClientCancelPaymentRequest paymentRequest)
        {
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
    }
}
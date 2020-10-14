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
using PaymentGateway.Infrastructure.Handlers;

namespace PaymentGateway.API.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IHandlerManager _vendorHandlerManager;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger,
            IHandlerManager vendorHandlerManager)
        {
            _logger = logger;
            _vendorHandlerManager = vendorHandlerManager;
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

            var handler = _vendorHandlerManager.GetHandler(paymentRequest.PaymentType);
            return await handler.Payment(paymentRequest).ConfigureAwait(false);
        }

        [HttpPost("Cancel")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Refund([FromBody] ClientCancelPaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            var handler = _vendorHandlerManager.GetHandler(paymentRequest.PaymentType);
            return await handler.Refund(paymentRequest).ConfigureAwait(false);
        }


        [HttpPost("Query")]
        [ProducesResponseType(typeof(ApiResponse), Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), Status422UnprocessableEntity)]
        public async Task<ApiResponse> Inquiry([FromBody] ClientInquiryRequest inquiryRequest)
        {
            if (!ModelState.IsValid) { throw new ApiProblemDetailsException(ModelState); }

            var handler = _vendorHandlerManager.GetHandler(inquiryRequest.PaymentType);
            return await handler.Inquiry(inquiryRequest).ConfigureAwait(false);
        }
    }
}

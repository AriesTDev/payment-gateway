using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Core.Constant;
using PaymentGateway.DTO.Request;

namespace PaymentGateway.DTO.RequestExtensions
{
    public static class GetRequestType
    {
        public static PaymentRequest GetPaymentType(this ClientPaymentRequest request, string returnUrl = null)
        {
            if (!request.PaymentType.Contains("alipay")) return new WechatOnlinePayPaymentRequest();
            if (string.IsNullOrEmpty(returnUrl))
            {
                return new AlipayOnlinePayPaymentRequest();
            }
            return new AlipayOnlinePayPaymentRequest
            {
                ReturnUrl = returnUrl
            };
        }
    }
}

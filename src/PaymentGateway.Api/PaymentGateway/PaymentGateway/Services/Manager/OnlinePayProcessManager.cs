using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Contracts.Manager;
using PaymentGateway.DTO.Request;
using PaymentGateway.DTO.Response;

namespace PaymentGateway.Services.Manager
{
    public class OnlinePayProcessManager : IOnlinePayProcessManager
    {
        public async Task PreparePaymentRequest()
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentResponseToClient> ProcessAlipayOnlinePayment(AlipayOnlinePayPaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

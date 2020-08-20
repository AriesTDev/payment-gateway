using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aitea.PaymentGateway.Contracts.Manager;
using Aitea.PaymentGateway.DTO.Request;
using Aitea.PaymentGateway.DTO.Response;

namespace Aitea.PaymentGateway.Services.Manager
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

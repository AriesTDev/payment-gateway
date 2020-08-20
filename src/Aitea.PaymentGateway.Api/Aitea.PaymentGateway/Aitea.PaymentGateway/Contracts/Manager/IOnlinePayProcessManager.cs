using System.Threading.Tasks;
using Aitea.PaymentGateway.DTO.Request;
using Aitea.PaymentGateway.DTO.Response;

namespace Aitea.PaymentGateway.Contracts.Manager
{
    public  interface IOnlinePayProcessManager
    {
        Task PreparePaymentRequest();
        Task<PaymentResponseToClient> ProcessAlipayOnlinePayment(AlipayOnlinePayPaymentRequest request);
    }
}

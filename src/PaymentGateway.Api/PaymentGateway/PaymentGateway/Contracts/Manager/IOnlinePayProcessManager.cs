using System.Threading.Tasks;
using PaymentGateway.DTO.Request;
using PaymentGateway.DTO.Response;

namespace PaymentGateway.Contracts.Manager
{
    public  interface IOnlinePayProcessManager
    {
        Task PreparePaymentRequest();
        Task<PaymentResponseToClient> ProcessAlipayOnlinePayment(AlipayOnlinePayPaymentRequest request);
    }
}

using AutoWrapper.Wrappers;
using PaymentGateway.DTO.Request;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Handlers
{
    public interface IHandler
    {
        Task<ApiResponse> Payment(ClientPaymentRequest paymentReques);
        Task<ApiResponse> Refund(ClientCancelPaymentRequest paymentRequest);
        Task<ApiResponse> Inquiry(ClientInquiryRequest inquiryRequest);
    }
}

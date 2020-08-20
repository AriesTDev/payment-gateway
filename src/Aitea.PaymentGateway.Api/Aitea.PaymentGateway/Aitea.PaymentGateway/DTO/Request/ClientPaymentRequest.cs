using FluentValidation;

namespace Aitea.PaymentGateway.DTO.Request
{
    public class ClientPaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentType { get; set; }
        public string TransactionNumber { get; set; }
        public string GoodsName { get; set; }
    }
    public class ClientPaymentRequestValidator : AbstractValidator<ClientPaymentRequest>
    {
        public ClientPaymentRequestValidator()
        {
            RuleFor(o => o.Amount).NotEmpty().GreaterThan(0);
            RuleFor(o => o.PaymentType).NotEmpty();
            RuleFor(o => o.TransactionNumber).NotEmpty();
        }
    }
}

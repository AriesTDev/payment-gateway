using FluentValidation;

namespace PaymentGateway.DTO.Request
{
    public class ClientCancelPaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentType { get; set; }
        public string TransactionNumber { get; set; }
        public string PgTransactionNumber { get; set; }
        public string GoodsName { get; set; }
    }
    public class ClientCancelPaymentRequestValidator : AbstractValidator<ClientCancelPaymentRequest>
    {
        public ClientCancelPaymentRequestValidator()
        {
            RuleFor(o => o.Amount).NotEmpty().GreaterThan(0);
            RuleFor(o => o.PaymentType).NotEmpty();
            RuleFor(o => o.TransactionNumber).NotEmpty();
        }
    }
}

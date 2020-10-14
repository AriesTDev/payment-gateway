using FluentValidation;
using System;

namespace PaymentGateway.DTO.Request
{
    public class ClientInquiryRequest
    {
        public string PaymentType { get; set; }
        public string PgTransactionNumber { get; set; }
        public string TransactionNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    public class ClientInquiryRequestValidator : AbstractValidator<ClientInquiryRequest>
    {
        public ClientInquiryRequestValidator()
        {
            RuleFor(o => o.TransactionNumber).NotEmpty().When(t => string.IsNullOrEmpty(t.PgTransactionNumber));
            RuleFor(o => o.PgTransactionNumber).NotEmpty().When(t => string.IsNullOrEmpty(t.TransactionNumber));
            RuleFor(o => o.StartTime).Must(BeAValidDate).When(t => !string.IsNullOrEmpty(t.StartTime));
            RuleFor(o => o.EndTime).Must(BeAValidDate).When(t => !string.IsNullOrEmpty(t.EndTime));
        }

        private bool BeAValidDate(string date)
        {
            if(!string.IsNullOrEmpty(date))
            {
                DateTime result;
                DateTime.TryParse(date, out result);
                if (result == default(DateTime))
                    return false;
            }
            return true;
        }
    }
}

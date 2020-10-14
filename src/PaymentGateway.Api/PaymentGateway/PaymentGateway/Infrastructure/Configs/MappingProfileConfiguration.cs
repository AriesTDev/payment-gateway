using PaymentGateway.Core.Constant;
using PaymentGateway.Core.Extensions;
using PaymentGateway.DTO.Request;
using PaymentGateway.DTO.Response;
using AutoMapper;

namespace PaymentGateway.Infrastructure.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<ClientPaymentRequest, AlipayOnlinePayPaymentRequest>()
                .ForMember(
                    a=>a.Amount,
                    c=>c.MapFrom(b=>b.Amount.ToCents())
                    )
                .ForMember(a => a.PaymentType,
                    c => c.MapFrom(b => b.PaymentType.GetQFPayPaymentTypeCode()));

            CreateMap<ClientPaymentRequest, WechatOnlinePayPaymentRequest>()
                .ForMember(
                    a => a.Amount,
                    c => c.MapFrom(b => b.Amount.ToCents())
                )
                .ForMember(a => a.PaymentType,
                    c => c.MapFrom(b => b.PaymentType.GetQFPayPaymentTypeCode())
                    );

            CreateMap<ClientCancelPaymentRequest, CancelPaymentRequest>()
                .ForMember(
                    a => a.RefundAmount,
                    c => c.MapFrom(b => b.Amount.ToCents())
                )
                .ForMember(
                    a => a.QFPayTransactionNumber,
                    c => c.MapFrom(b => b.PgTransactionNumber)
                    );
                
            CreateMap<PaymentResponse, PaymentResponseToClient>()
                .ForMember(d => d.ErrorCode,
                    s => s.MapFrom(b => b.ResponseCode))
                .ForMember(d => d.ErrorMessage,
                    s => s.MapFrom(b => b.ResponseError))
                .ForMember(d => d.Status,
                    s => s.MapFrom(b => b.ResponseCode.GetPaymentStatus()));

            CreateMap<CancelPaymentResponse, CancelPaymentResponseToClient>()
               .ForMember(d => d.ErrorCode,
                   s => s.MapFrom(b => b.ReturnCode))
               .ForMember(d => d.ErrorMessage,
                   s => s.MapFrom(b => b.ResponseMessage))
               .ForMember(d => d.Status,
                   s => s.MapFrom(b => b.ReturnCode.GetPaymentStatus()));


            CreateMap<ClientInquiryRequest, InquirePaymentRequest>()
                .ForMember(a => a.PaymentType,
                    c => c.MapFrom(b => b.PaymentType.GetQFPayPaymentTypeCode()))
                .ForMember(a => a.StartTime,
                    c => c.MapFrom(b => string.IsNullOrEmpty(b.StartTime) ? null : b.StartTime))
                .ForMember(a => a.EndTime,
                    c => c.MapFrom(b => string.IsNullOrEmpty(b.EndTime) ? null : b.EndTime))
                .ForMember(a => a.QFPayTransactionNumber,
                    c => c.MapFrom(b => string.IsNullOrEmpty(b.PgTransactionNumber) ? null : b.PgTransactionNumber))
                .ForMember(a => a.TransactionNumber,
                    c => c.MapFrom(b => string.IsNullOrEmpty(b.TransactionNumber) ? null : b.TransactionNumber)
                    );

            CreateMap<InquirePaymentResponse, InquirePaymentResponseToClient>()
               .ForMember(d => d.ErrorCode,
                   s => s.MapFrom(b => b.ReturnCode))
               .ForMember(d => d.ErrorMessage,
                   s => s.MapFrom(b => b.StatusMessage))
               .ForMember(d => d.Status,
                   s => s.MapFrom(b => b.ReturnCode.GetPaymentStatus()));

        }
    }
}

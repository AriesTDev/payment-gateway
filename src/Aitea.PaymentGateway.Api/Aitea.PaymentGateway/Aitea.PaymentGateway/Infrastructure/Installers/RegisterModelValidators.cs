using Aitea.PaymentGateway.Contracts;
using Aitea.PaymentGateway.DTO.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aitea.PaymentGateway.Infrastructure.Installers
{
    internal class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register DTO Validators
            services.AddTransient<IValidator<ClientPaymentRequest>, ClientPaymentRequestValidator>();
            services.AddTransient<IValidator<ClientInquiryRequest>, ClientInquiryRequestValidator>();
            services.AddTransient<IValidator<ClientCancelPaymentRequest>, ClientCancelPaymentRequestValidator>();

            //Disable Automatic Model State Validation built-in to ASP.NET Core
            services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });
        }
    }
}

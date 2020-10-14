using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Contracts;
using PaymentGateway.Contracts.Manager;
using PaymentGateway.DTO.Request;
using PaymentGateway.Services.Manager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentGateway.Infrastructure.Installers
{
    internal class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IOnlinePayProcessManager,OnlinePayProcessManager>();
        }
    }
}

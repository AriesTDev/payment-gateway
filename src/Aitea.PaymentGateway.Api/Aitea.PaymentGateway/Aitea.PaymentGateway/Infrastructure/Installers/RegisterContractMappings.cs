using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aitea.PaymentGateway.Contracts;
using Aitea.PaymentGateway.Contracts.Manager;
using Aitea.PaymentGateway.DTO.Request;
using Aitea.PaymentGateway.Services.Manager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aitea.PaymentGateway.Infrastructure.Installers
{
    internal class RegisterContractMappings : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IOnlinePayProcessManager,OnlinePayProcessManager>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Configs
{
    public class QfPayConfiguration
    {
        public string MerchantId { get; set; }
        public string AppCode { get; set; }
        public string ApiKey { get; set; }
    }
}

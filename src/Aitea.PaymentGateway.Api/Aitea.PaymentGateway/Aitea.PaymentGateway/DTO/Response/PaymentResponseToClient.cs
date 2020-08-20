using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aitea.PaymentGateway.DTO.Response
{
    public class PaymentResponseToClient
    {
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

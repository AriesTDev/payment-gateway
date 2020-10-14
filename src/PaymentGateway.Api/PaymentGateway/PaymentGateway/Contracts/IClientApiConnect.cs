using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Ocsp;

namespace PaymentGateway.Contracts
{
    public interface IClientApiConnect
    {
        Task<TResponse> GetTransaction<TResponse,TRequest>(TRequest model);
        Task<TResponse> UpdateTransaction<TResponse,TRequest>(TRequest model);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Aitea.PaymentGateway.Contracts;

namespace Aitea.PaymentGateway.Services
{
    public class AiteaClientApiConnect : BaseApiConnect, IAiteaClientApiConnect
    {
        public AiteaClientApiConnect(HttpClient aiTeaHttpClient) : base(aiTeaHttpClient)
        {
        }
        public async Task<TResponse> GetTransaction<TResponse, TRequest>(TRequest model)
        {
            throw new NotImplementedException();
        }

        public async Task<TResponse> UpdateTransaction<TResponse, TRequest>(TRequest model)
        {
            throw new NotImplementedException();
        }
    }
}

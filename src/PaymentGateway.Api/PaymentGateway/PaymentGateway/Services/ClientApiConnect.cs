using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PaymentGateway.Contracts;

namespace PaymentGateway.Services
{
    public class ClientApiConnect : BaseApiConnect, IClientApiConnect
    {
        public ClientApiConnect(HttpClient aiTeaHttpClient) : base(aiTeaHttpClient)
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

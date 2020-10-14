using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class BaseApiConnect
    {
        protected readonly HttpClient AiTeaHttpClient;

        public BaseApiConnect(HttpClient aiTeaHttpClient)
        {
            AiTeaHttpClient = aiTeaHttpClient;
        }
    }
}

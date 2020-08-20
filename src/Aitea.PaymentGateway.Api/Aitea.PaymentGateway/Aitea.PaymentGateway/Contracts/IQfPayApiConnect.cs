using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aitea.PaymentGateway.Contracts
{
    public interface IQfPayApiConnect
    {
        Task<TResponse> PostDataAsync<TResponse, TRequest>(string endPoint, TRequest request);
        Task<TResponse> PostStringDataAsync<TResponse, TRequest>(string endPoint, TRequest request, Dictionary<string,string> header=null);
        Task<TResponse> PostFormDataAsync<TResponse>(string endPoint, IDictionary<string,string> request,Dictionary<string,string> header=null);
        Task<TResponse> GetDataAsync<TResponse>(string endPoint);
    }
}

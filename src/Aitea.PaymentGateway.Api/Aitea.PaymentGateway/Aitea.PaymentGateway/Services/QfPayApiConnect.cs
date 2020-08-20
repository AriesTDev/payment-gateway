using System;
using System.Collections.Generic;
using Aitea.PaymentGateway.Contracts;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Aitea.PaymentGateway.Constants;
using Aitea.PaymentGateway.Infrastructure.Configs;
using Microsoft.Extensions.Logging;
using AutoWrapper.Server;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Aitea.PaymentGateway.Services
{
    public class QfPayApiConnect : BaseApiConnect, IQfPayApiConnect
    {
        private readonly ILogger<QfPayApiConnect> _logger;
        public QfPayApiConnect(HttpClient aiTeaHttpClient, ILogger<QfPayApiConnect> logger) : base(aiTeaHttpClient)
        {
            _logger = logger;
        }
        public async Task<TResponse> PostDataAsync<TResponse, TRequest>(string endPoint, TRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, HttpContentMediaTypes.JSON);
            var httpResponse = await AiTeaHttpClient.PostAsync(endPoint, content);

            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Warning, $"[{httpResponse.StatusCode}] An error occured while requesting external api.");
                return default;
            }
            var jsonString = await httpResponse.Content.ReadAsStringAsync();
            var data = Unwrapper.Unwrap<TResponse>(jsonString);

            return data;
        }

        public async Task<TResponse> PostStringDataAsync<TResponse, TRequest>(string endPoint, TRequest request,Dictionary<string,string> header=null)
        {
            var content = new StringContent(request.ToString(),Encoding.UTF8, HttpContentMediaTypes.TEXT);
            if (header?.Count>0)
            {
                foreach (var key in header.Keys)
                {
                    content.Headers.Add(key, header[key]);
                }
            }
            using var result = await AiTeaHttpClient.PostAsync(endPoint, content).ConfigureAwait(false);
            var response = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"server response not successful. {result.StatusCode}, {response}");
            }

            var obj = JsonConvert.DeserializeObject<TResponse>(response);
            if (obj == null)
                throw new ApplicationException($"Failed to serialize server response \"{response}\"");
            return obj;
        }

        public async Task<TResponse> PostFormDataAsync<TResponse>(string endPoint, IDictionary<string, string> request, Dictionary<string, string> header=null)
        {
            _logger.LogInformation($"Post Form request. URL {endPoint}, data: {JsonConvert.SerializeObject(request)}");
            var req = new FormUrlEncodedContent(request);
            if (header?.Count > 0)
            {
                foreach (var key in header.Keys)
                {
                    req.Headers.Add(key, header[key]);
                }
            }
            using (var result = await AiTeaHttpClient.PostAsync(endPoint, req).ConfigureAwait(false))
            {
                var response = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!result.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Server response not successful. {result.StatusCode}, {response}");
                }

                var obj = JsonConvert.DeserializeObject<TResponse>(response);
                if (obj == null)
                    throw new ApplicationException($"Failed to serialize server response \"{response}\"");
                _logger.LogInformation($"Post Form response: {JsonConvert.SerializeObject(response)}");
                return obj;
            }
        }

        public async Task<TResponse> GetDataAsync<TResponse>(string endPoint)
        {
            var httpResponse = await AiTeaHttpClient.GetAsync(endPoint);

            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Warning, $"[{httpResponse.StatusCode}] An error occured while requesting external api.");
                return default;
            }

            var jsonString = await httpResponse.Content.ReadAsStringAsync();
            var data = Unwrapper.Unwrap<TResponse>(jsonString);

            return data;
        }

      
    }
}

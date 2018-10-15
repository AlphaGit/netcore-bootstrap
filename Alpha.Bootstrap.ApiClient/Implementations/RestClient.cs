using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Alpha.Bootstrap.ApiClient.Implementations
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUri;
        private readonly IDictionary<string, string> _headers = new Dictionary<string, string>();

        public RestClient(Configuration configuration)
        {
            _baseUri = configuration.BaseUri;
            _httpClient = new HttpClient();
        }

        public async Task<RestResponse<TResource>> GetAsync<TResource>(string relativeUrl)
        {
            return await RequestWithResponse<TResource>(HttpMethod.Get, relativeUrl, null);
        }

        public async Task<RestResponse<TResource>> PostAsync<TResource>(string relativeUrl, object body)
        {
            var httpContent = SerializeBody(body);
            return await RequestWithResponse<TResource>(HttpMethod.Post, relativeUrl, httpContent);
        }

        public async Task<RestResponse<TResource>> PutAsync<TResource>(string relativeUrl, object body)
        {
            var httpContent = SerializeBody(body);
            return await RequestWithResponse<TResource>(HttpMethod.Put, relativeUrl, httpContent);
        }

        public async Task<RestResponse<TResource>> DeleteAsync<TResource>(string relativeUrl)
        {
            return await RequestWithResponse<TResource>(HttpMethod.Delete, relativeUrl, null);
        }

        public async Task<RestResponse> PostAsync(string relativeUrl, object body)
        {
            var httpContent = SerializeBody(body);
            return await RequestWithNoResponse(HttpMethod.Post, relativeUrl, httpContent);
        }

        public async Task<RestResponse> PutAsync(string relativeUrl, object body)
        {
            var httpContent = SerializeBody(body);
            return await RequestWithNoResponse(HttpMethod.Put, relativeUrl, httpContent);
        }

        public async Task<RestResponse> DeleteAsync(string relativeUrl)
        {
            return await RequestWithNoResponse(HttpMethod.Delete, relativeUrl, null);
        }

        public void AddOrUpdateHeader(string headerName, string headerValue)
        {
            _headers[headerName] = headerValue;
        }

        private HttpContent SerializeBody(object body)
        {
            if (body == null) return null;

            var serializedObject = JsonConvert.SerializeObject(body, Formatting.None);
            return new StringContent(serializedObject, Encoding.UTF8, "application/json");
        }

        private async Task<RestResponse<TResource>> RequestWithResponse<TResource>(HttpMethod method,
            string relativeUrl, HttpContent content)
        {
            var httpResponse = await ExecuteRequest(method, relativeUrl, content);
            var response = await ParseResponse<TResource>(httpResponse);
            return new RestResponse<TResource>(httpResponse.StatusCode, response);
        }

        private async Task<RestResponse> RequestWithNoResponse(HttpMethod method, string relativeUrl,
            HttpContent content)
        {
            var httpResponse = await ExecuteRequest(method, relativeUrl, content);
            return new RestResponse(httpResponse.StatusCode);
        }

        private async Task<HttpResponseMessage> ExecuteRequest(HttpMethod method, string relativeUrl,
            HttpContent content)
        {
            var request = PrepareRequest(method, relativeUrl, content);
            return await _httpClient.SendAsync(request);
        }

        private HttpRequestMessage PrepareRequest(HttpMethod method, string relativeUrl, HttpContent requestBody)
        {
            if (string.IsNullOrEmpty(relativeUrl) || !Uri.TryCreate(_baseUri, relativeUrl, out Uri absoluteUrl))
                throw new ArgumentException($"Invalid relative url: {_baseUri}{relativeUrl}", nameof(relativeUrl));

            var requestMessage = new HttpRequestMessage
            {
                RequestUri = absoluteUrl,
                Method = method,
                Content = requestBody
            };

            foreach (var header in _headers)
                requestMessage.Headers.Add(header.Key, header.Value);

            return requestMessage;
        }

        private async Task<TResource> ParseResponse<TResource>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResource>(responseContent);
        }
    }
}

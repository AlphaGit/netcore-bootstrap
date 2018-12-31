using System.Net;
using System.Net.Http;

namespace Alpha.Bootstrap.ApiClient
{
    public class RestResponse
    {
        public HttpStatusCode StatusCode => HttpResponse.StatusCode;
        public HttpResponseMessage HttpResponse { get; }

        public RestResponse(HttpResponseMessage httpResponse)
        {
            HttpResponse = httpResponse;
        }
    }

    public class RestResponse<TResource> : RestResponse
    {
        public TResource Response { get; }

        public RestResponse(HttpResponseMessage httpResponse, TResource response)
            : base(httpResponse)
        {
            Response = response;
        }
    }
}

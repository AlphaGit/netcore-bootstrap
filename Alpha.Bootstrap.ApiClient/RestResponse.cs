using System.Net;

namespace Alpha.Bootstrap.ApiClient
{
    public class RestResponse
    {
        public HttpStatusCode StatusCode { get; }

        public RestResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }

    public class RestResponse<TResource> : RestResponse
    {
        public TResource Response { get; }

        public RestResponse(HttpStatusCode statusCode, TResource response)
            : base(statusCode)
        {
            Response = response;
        }
    }
}

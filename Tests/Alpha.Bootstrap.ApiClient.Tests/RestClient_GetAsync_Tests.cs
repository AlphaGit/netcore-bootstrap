using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alpha.Bootstrap.ApiClient.Implementations;
using RichardSzalay.MockHttp;
using Xunit;

namespace Alpha.Bootstrap.ApiClient.Tests
{
    public class RestClient_GetAsync_Tests
    {
        private readonly RestClient _restClient;
        private readonly MockHttpMessageHandler _mockHttpClient;

        public RestClient_GetAsync_Tests()
        {
            var configuration = new Configuration()
            {
                BaseUri = new Uri("https://localhost/")
            };

            _mockHttpClient = new MockHttpMessageHandler();

            _restClient = new RestClient(configuration, _mockHttpClient.ToHttpClient());
        }

        private class ExampleDto
        {
            public string Name { get; set; }

            public string Title { get; set; }
        }

        [Fact]
        public async Task SendsGetCallToSpecifiedRelativeUrl()
        {
            // Arrange.
            var expectedResponse = "{}";
            var expectedRequest = _mockHttpClient.When(HttpMethod.Get, "https://localhost/exampleUrl")
                .Respond("application/json", expectedResponse);

            // Act.
            await _restClient.GetAsync<ExampleDto>("exampleUrl");

            // Assert.
            var endpointCalledTimes = _mockHttpClient.GetMatchCount(expectedRequest);
            Assert.Equal(1, endpointCalledTimes);
        }

        [Fact]
        public async Task DeserializesContent()
        {
            // Arrange.
            var expectedResponse = "{'name': 'Example Name', 'title': 'Example Title'}";
            _mockHttpClient.When(HttpMethod.Get, "https://localhost/exampleUrl")
                .Respond("application/json", expectedResponse);

            // Act.
            var response = await _restClient.GetAsync<ExampleDto>("exampleUrl");

            // Assert.
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Response);
            var parsedResponse = response.Response;
            Assert.Equal("Example Name", parsedResponse.Name);
            Assert.Equal("Example Title", parsedResponse.Title);
        }
    }
}

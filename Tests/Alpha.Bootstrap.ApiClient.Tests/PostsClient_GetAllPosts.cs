using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alpha.Bootstrap.ApiClient.Implementations;
using Alpha.Bootstrap.ApiClient.Tests.Fakers;
using Alpha.Bootstrap.WebApi.Dtos.v1;
using Moq;
using Xunit;

namespace Alpha.Bootstrap.ApiClient.Tests
{
    public class PostsClient_GetAllPosts
    {
        private readonly Mock<IRestClient> _restClientMock;

        private readonly PostsClient _postsClient;

        public PostsClient_GetAllPosts()
        {
            _restClientMock = new Mock<IRestClient>();

            _postsClient = new PostsClient(_restClientMock.Object);
        }

        [Fact]
        public async Task SendsAPostRequestToThePostsUrl()
        {
            // Arrange.
            var responseContents = new GetAllPostsResponseFaker().Generate();

            var expectedResponse = new RestResponse<GetAllPostsResponse>(new HttpResponseMessage(HttpStatusCode.OK), responseContents);

            _restClientMock.Setup(r => r.GetAsync<GetAllPostsResponse>("posts"))
                .ReturnsAsync(expectedResponse)
                .Verifiable();

            // Act.
            var posts = await _postsClient.GetAllPosts();

            // Assert.
            Assert.Equal(expectedResponse, posts);
            _restClientMock.Verify();
        }
    }
}

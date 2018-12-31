using System.Net;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL.Models;
using Xunit;

namespace Alpha.Bootstrap.IntegrationTests
{
    public class Posts_GetAllPosts
    {
        private readonly ApiTestServer _testServer;
        private readonly ApiClient.Implementations.ApiClient _apiClient;

        public Posts_GetAllPosts()
        {
            _testServer = new ApiTestServer();
            _apiClient = _testServer.GetApiClient();
        }

        [Fact]
        public async Task ReturnsAllPosts()
        {
            // Arrange.
            var dbContext = _testServer.GetBlogDbContext();
            dbContext.Posts.Add(new Post());
            await dbContext.SaveChangesAsync();

            // Act.
            var restResponse = await _apiClient.PostsClient.GetAllPosts();

            // Assert.
            Assert.NotNull(restResponse);
            Assert.Equal(HttpStatusCode.OK, restResponse.StatusCode);

            var getAllPostsResponse = restResponse.Response;
            Assert.NotNull(getAllPostsResponse);

            var posts = getAllPostsResponse.Posts;
            Assert.NotNull(posts);
            Assert.NotEmpty(posts);
        }

        [Fact]
        public async Task WhenNoPosts_ReturnsEmpty()
        {
            // Arrange.

            // Act.
            var restResponse = await _apiClient.PostsClient.GetAllPosts();

            // Assert.
            Assert.NotNull(restResponse);
            Assert.Equal(HttpStatusCode.OK, restResponse.StatusCode);

            var getAllPostsResponse = restResponse.Response;
            Assert.NotNull(getAllPostsResponse);

            var posts = getAllPostsResponse.Posts;
            Assert.NotNull(posts);
            Assert.Empty(posts);
        }
    }
}

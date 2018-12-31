using System;
using System.Net;
using System.Threading.Tasks;
using Alpha.Bootstrap.WebApi.Dtos.v1;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Alpha.Bootstrap.IntegrationTests
{
    public class Posts_Create
    {
        private readonly ApiTestServer _testServer;
        private readonly ApiClient.Implementations.ApiClient _apiClient;

        public Posts_Create()
        {
            _testServer = new ApiTestServer();
            _apiClient = _testServer.GetApiClient();
        }

        [Fact]
        public async Task CreatesNewPost()
        {
            // Arrange.
            var newPost = new CreatePostRequest()
            {
                Title = "new title",
                Content = "some content",
            };

            // Act.
            var restResponse = await _apiClient.PostsClient.Create(newPost);

            // Assert.
            Assert.NotNull(restResponse);
            Assert.Equal(HttpStatusCode.Created, restResponse.StatusCode);

            var location = restResponse.HttpResponse.Headers.Location;
            Assert.NotNull(location);

            // Location: scheme://host:port/api/posts/[post-id]?possibleQuery
            var path = location.GetComponents(UriComponents.Path, UriFormat.Unescaped);
            var postIdString = path.Substring(path.LastIndexOf('/') + 1);
            var postId = Guid.Parse(postIdString);

            var dbContext2 = _testServer.GetBlogDbContext();
            Assert.True(await dbContext2.Posts.AnyAsync(p => p.Id == postId));
        }
    }
}

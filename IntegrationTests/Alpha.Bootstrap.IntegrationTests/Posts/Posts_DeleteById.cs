using System;
using System.Net;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Alpha.Bootstrap.IntegrationTests.Posts
{
    public class Posts_DeleteById
    {
        private readonly ApiTestServer _testServer;
        private readonly ApiClient.Implementations.ApiClient _apiClient;

        public Posts_DeleteById()
        {
            _testServer = new ApiTestServer();
            _apiClient = _testServer.GetApiClient();
        }

        [Fact]
        public async Task DeletesExistingPost()
        {
            // Arrange.
            var dbContext = _testServer.GetBlogDbContext();
            var post = new Post();
            dbContext.Posts.Add(post);
            await dbContext.SaveChangesAsync();

            // Act.
            var restResponse = await _apiClient.PostsClient.Delete(post.Id);

            // Assert.
            Assert.NotNull(restResponse);
            Assert.Equal(HttpStatusCode.NoContent, restResponse.StatusCode);

            var dbContext2 = _testServer.GetBlogDbContext();
            Assert.False(await dbContext2.Posts.AnyAsync(p => p.Id == post.Id));
        }

        [Fact]
        public async Task SupportsCaseWherePostDidNotExist()
        {
            // Arrange.

            // Act.
            var restResponse = await _apiClient.PostsClient.Delete(Guid.NewGuid());

            // Assert.
            Assert.NotNull(restResponse);
            Assert.Equal(HttpStatusCode.NoContent, restResponse.StatusCode);

            var dbContext2 = _testServer.GetBlogDbContext();
            Assert.False(await dbContext2.Posts.AnyAsync());
        }
    }
}

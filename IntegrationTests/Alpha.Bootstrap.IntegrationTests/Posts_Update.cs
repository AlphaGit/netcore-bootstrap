using System;
using System.Net;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL.Models;
using Alpha.Bootstrap.WebApi.Dtos.v1;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Alpha.Bootstrap.IntegrationTests
{
    public class Posts_Update
    {
        private readonly ApiTestServer _testServer;
        private readonly ApiClient.Implementations.ApiClient _apiClient;

        public Posts_Update()
        {
            _testServer = new ApiTestServer();
            _apiClient = _testServer.GetApiClient();
        }

        [Fact]
        public async Task UpdatesExistingPost()
        {
            // Arrange.
            var dbContext = _testServer.GetBlogDbContext();
            var post = new Post();
            dbContext.Posts.Add(post);
            await dbContext.SaveChangesAsync();

            var postUpdate = new UpdatePostRequest()
            {
                Title = "new title",
                Content = "some new content",
            };

            // Act.
            var restResponse = await _apiClient.PostsClient.Update(post.Id, postUpdate);

            // Assert.
            Assert.NotNull(restResponse);
            Assert.Equal(HttpStatusCode.NoContent, restResponse.StatusCode);

            var dbContext2 = _testServer.GetBlogDbContext();
            var postInDb = await dbContext2.Posts.SingleAsync(p => p.Id == post.Id);

            Assert.Equal(postUpdate.Content, postInDb.Content);
            Assert.Equal(postUpdate.Title, postInDb.Title);
        }
    }
}

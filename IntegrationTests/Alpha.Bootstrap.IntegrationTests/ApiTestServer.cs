using System;
using System.Collections.Generic;
using Alpha.Bootstrap.ApiClient;
using Alpha.Bootstrap.ApiClient.Implementations;
using Alpha.Bootstrap.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Alpha.Bootstrap.IntegrationTests
{
    class ApiTestServer : IDisposable
    {
        private readonly TestServer _testServer;

        private List<IDisposable> _toDispose = new List<IDisposable>();

        public ApiTestServer()
        {
            var webHost = new WebHostBuilder()
                .UseSolutionRelativeContentRoot("Alpha.Bootstrap.WebApi")
                .UseStartup<TestStartup>();

            _testServer = new TestServer(webHost);
            _toDispose.Add(_testServer);
        }

        public BlogDbContext GetBlogDbContext()
        {
            var dbContext = _testServer.Host.Services.GetService<BlogDbContext>();
            _toDispose.Add(dbContext);

            return dbContext;
        }

        public ApiClient.Implementations.ApiClient GetApiClient()
        {
            var apiClientConfiguration = new Configuration()
            {
                BaseUri = new Uri(_testServer.BaseAddress, "api/")
            };

            var restClient = new RestClient(apiClientConfiguration, _testServer.CreateClient());

            return new ApiClient.Implementations.ApiClient(restClient, new AuthenticationClient(), new PostsClient(restClient));
        }

        public void Dispose()
        {
            _toDispose?.ForEach(d => d?.Dispose());
        }
    }
}

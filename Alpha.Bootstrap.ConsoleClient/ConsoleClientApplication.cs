using System;
using System.Threading.Tasks;
using Alpha.Bootstrap.ApiClient;

namespace Alpha.Bootstrap.ConsoleClient
{
    internal class ConsoleClientApplication : IConsoleClientApplication
    {
        private readonly IApiClient _apiClient;

        public ConsoleClientApplication(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task Run()
        {
            Console.WriteLine("Getting all posts...");

            var allPostsResponse = await _apiClient.PostsClient.GetAllPosts();

            Console.WriteLine($"Response code: {allPostsResponse.StatusCode}.");

            var posts = allPostsResponse.Response.Posts;
            Console.WriteLine($"Posts in response: {posts.Count}");

            Console.WriteLine();
            Console.WriteLine("------");
            foreach (var post in posts)
            {
                Console.WriteLine(post.Id);
                Console.WriteLine(post.Title);
                Console.WriteLine(post.Content);
                Console.WriteLine("------");
            }

            Console.WriteLine();
            Console.Write("Press any key to finish...");
            Console.ReadKey();
        }
    }
}
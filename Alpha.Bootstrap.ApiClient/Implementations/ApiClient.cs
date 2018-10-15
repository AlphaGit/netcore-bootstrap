namespace Alpha.Bootstrap.ApiClient.Implementations
{
    public class ApiClient : IApiClient
    {
        public ApiClient(IRestClient restClient, IAuthenticationClient authenticationClient, IPostsClient postsClient)
        {
            RestClient = restClient;
            AuthenticationClient = authenticationClient;
            PostsClient = postsClient;
        }

        public IRestClient RestClient { get; }

        public IAuthenticationClient AuthenticationClient { get; }

        public IPostsClient PostsClient { get; }
    }
}

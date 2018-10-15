namespace Alpha.Bootstrap.ApiClient
{
    public interface IApiClient
    {
        IRestClient RestClient { get; }

        IAuthenticationClient AuthenticationClient { get; }

        IPostsClient PostsClient { get; }
    }
}

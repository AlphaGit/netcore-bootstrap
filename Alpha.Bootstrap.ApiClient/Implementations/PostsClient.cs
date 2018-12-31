using System;
using System.Threading.Tasks;
using Alpha.Bootstrap.WebApi.Dtos.v1;

namespace Alpha.Bootstrap.ApiClient.Implementations
{
    public class PostsClient : IPostsClient
    {
        private readonly IRestClient _restClient;

        public PostsClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<RestResponse<GetAllPostsResponse>> GetAllPosts()
        {
            return await _restClient.GetAsync<GetAllPostsResponse>("posts");
        }

        public async Task<RestResponse> Delete(Guid id)
        {
            return await _restClient.DeleteAsync($"posts/{id}");
        }
    }
}

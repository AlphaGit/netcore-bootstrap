using System;
using System.Threading.Tasks;
using Alpha.Bootstrap.WebApi.Dtos.v1;

namespace Alpha.Bootstrap.ApiClient
{
    public interface IPostsClient
    {
        Task<RestResponse<GetAllPostsResponse>> GetAllPosts();

        Task<RestResponse> Delete(Guid id);

        Task<RestResponse> Create(CreatePostRequest newPost);
    }
}
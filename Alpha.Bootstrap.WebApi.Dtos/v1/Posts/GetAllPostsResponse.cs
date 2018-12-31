using System.Collections.Generic;

namespace Alpha.Bootstrap.WebApi.Dtos.v1.Posts
{
    public class GetAllPostsResponse
    {
        public ICollection<PostDto> Posts { get; set; }
    }
}

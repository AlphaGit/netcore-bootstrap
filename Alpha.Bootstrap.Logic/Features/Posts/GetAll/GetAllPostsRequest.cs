using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetAll
{
    public class GetAllPostsRequest : IRequest<GetAllPostsResponse>
    {
        public string Filter { get; set; }
    }
}

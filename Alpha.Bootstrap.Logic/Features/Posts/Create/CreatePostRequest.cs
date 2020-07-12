using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.Create
{
    public class CreatePostRequest : IRequest<CreatePostResponse>
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

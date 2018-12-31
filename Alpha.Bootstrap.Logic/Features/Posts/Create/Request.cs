using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.Create
{
    public class Request : IRequest<Response>
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

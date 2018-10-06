using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetAll
{
    public class Request : IRequest<Response>
    {
        public string Filter { get; set; }
    }
}

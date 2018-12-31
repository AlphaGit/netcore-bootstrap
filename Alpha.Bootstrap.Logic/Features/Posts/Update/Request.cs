using Alpha.Bootstrap.Logic.Models;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.Update
{
    public class Request : IRequest<Response>
    {
        public Post Post { get; set; }
    }
}

using Alpha.Bootstrap.Logic.Models;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.Update
{
    public class UpdatePostRequest : IRequest<UpdatePostResponse>
    {
        public Post Post { get; set; }
    }
}

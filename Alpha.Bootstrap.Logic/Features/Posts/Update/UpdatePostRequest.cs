using Alpha.Bootstrap.Logic.Models;
using FluentResults;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.Update
{
    public class UpdatePostRequest : IRequest<Result<Post>>
    {
        public Post Post { get; set; }
    }
}

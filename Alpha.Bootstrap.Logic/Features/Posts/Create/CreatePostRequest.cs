using Alpha.Bootstrap.Logic.Models;
using FluentResults;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.Create
{
    public class CreatePostRequest : IRequest<Result<Post>>
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

using System.Collections.Generic;
using Alpha.Bootstrap.Logic.Models;
using FluentResults;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetAll
{
    public class GetAllPostsRequest : IRequest<Result<ICollection<Post>>>
    {
        public string Filter { get; set; }
    }
}

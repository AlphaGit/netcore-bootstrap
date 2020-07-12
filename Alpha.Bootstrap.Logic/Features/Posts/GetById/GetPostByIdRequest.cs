using System;
using Alpha.Bootstrap.Logic.Models;
using FluentResults;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetById
{
    public class GetPostByIdRequest : IRequest<Result<Post>>
    {
        public Guid Id { get; set; }
    }
}

using System;
using FluentResults;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.DeleteById
{
    public class DeletePostByIdRequest : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

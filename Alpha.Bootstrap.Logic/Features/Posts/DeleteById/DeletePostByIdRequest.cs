using System;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.DeleteById
{
    public class DeletePostByIdRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}

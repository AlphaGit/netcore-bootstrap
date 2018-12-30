using System;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetById
{
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
    }
}

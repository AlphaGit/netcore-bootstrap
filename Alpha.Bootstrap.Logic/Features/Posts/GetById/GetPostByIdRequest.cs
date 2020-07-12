using System;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetById
{
    public class GetPostByIdRequest : IRequest<GetPostByIdResponse>
    {
        public Guid Id { get; set; }
    }
}

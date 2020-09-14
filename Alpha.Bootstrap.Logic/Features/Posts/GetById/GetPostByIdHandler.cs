using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.Logic.Models;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetById
{
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdRequest, Result<Post>>
    {
        private readonly BlogDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPostByIdHandler(BlogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<Post>> Handle(GetPostByIdRequest getPostByIdRequest, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Id == getPostByIdRequest.Id, cancellationToken);

            var mappedPost = _mapper.Map<Post>(post);

            return Result.Ok(mappedPost);
        }
    }
}

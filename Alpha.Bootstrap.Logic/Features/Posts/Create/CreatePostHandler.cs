using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.DAL.Models;
using AutoMapper;
using FluentResults;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.Create
{
    public class CreatePostHandler : IRequestHandler<CreatePostRequest, Result<Models.Post>>
    {
        private readonly BlogDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreatePostHandler(BlogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<Models.Post>> Handle(CreatePostRequest createPostRequest, CancellationToken cancellationToken)
        {
            var inPost = _mapper.Map<Post>(createPostRequest);

            await _dbContext.Posts.AddAsync(inPost, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var outPost = _mapper.Map<Models.Post>(inPost);

            return Result.Ok(outPost);
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.DAL.Models;
using FluentResults;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.Create
{
    public class CreatePostHandler : IRequestHandler<CreatePostRequest, Result<Models.Post>>
    {
        private readonly BlogDbContext _dbContext;

        public CreatePostHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Models.Post>> Handle(CreatePostRequest createPostRequest, CancellationToken cancellationToken)
        {
            // TODO AutoMapper.
            var inPost = new Post()
            {
                Content = createPostRequest.Content,
                Title = createPostRequest.Title,
            };

            await _dbContext.Posts.AddAsync(inPost, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // TODO AutoMapper.
            var outPost = new Models.Post
            {
                Id = inPost.Id,
                Title = inPost.Title,
                Content = inPost.Content,
            };

            return Result.Ok(outPost);
        }
    }
}

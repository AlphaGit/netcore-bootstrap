using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.DAL.Models;
using Alpha.Bootstrap.Logic.Models.Errors;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.Update
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostRequest, Result<Models.Post>>
    {
        private readonly BlogDbContext _dbContext;

        public UpdatePostHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Models.Post>> Handle(UpdatePostRequest updatePostRequest, CancellationToken cancellationToken)
        {
            var postExists = await _dbContext.Posts.AnyAsync(p => p.Id == updatePostRequest.Post.Id, cancellationToken);
            if (!postExists)
                return Result.Fail<Models.Post>(new ResourceNotFoundError());

            // TODO AutoMapper.
            var inPost = new Post()
            {
                Content = updatePostRequest.Post.Content,
                Id = updatePostRequest.Post.Id,
                Title = updatePostRequest.Post.Title,
            };

            try
            {
                var trackingState = _dbContext.Attach(inPost);
                trackingState.State = EntityState.Modified;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Fail<Models.Post>(new ConcurrencyError());
            }

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

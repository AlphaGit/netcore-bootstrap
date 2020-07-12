using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.DAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.Update
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostRequest, UpdatePostResponse>
    {
        private readonly BlogDbContext _dbContext;

        public UpdatePostHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UpdatePostResponse> Handle(UpdatePostRequest updatePostRequest, CancellationToken cancellationToken)
        {
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
                return new UpdatePostResponse() { Post = null };
            }

            // TODO AutoMapper.
            var outPost = new Models.Post
            {
                Id = inPost.Id,
                Title = inPost.Title,
                Content = inPost.Content,
            };

            return new UpdatePostResponse() { Post = outPost };
        }
    }
}

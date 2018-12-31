using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.DAL.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.Update
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly BlogDbContext _dbContext;

        public Handler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            // TODO AutoMapper.
            var inPost = new Post()
            {
                Content = request.Post.Content,
                Id = request.Post.Id,
                Title = request.Post.Title,
            };

            try
            {
                var trackingState = _dbContext.Attach(inPost);
                trackingState.State = EntityState.Modified;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new Response() { Post = null };
            }

            // TODO AutoMapper.
            var outPost = new Models.Post
            {
                Id = inPost.Id,
                Title = inPost.Title,
                Content = inPost.Content,
            };

            return new Response() { Post = outPost };
        }
    }
}

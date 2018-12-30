using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetById
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
            var post = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            // TODO AutoMapper.
            var mappedPost = new Post
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
            };

            return new Response() { Post = mappedPost };
        }
    }
}

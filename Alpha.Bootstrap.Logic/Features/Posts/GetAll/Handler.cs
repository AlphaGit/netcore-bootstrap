using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetAll
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
            var posts = await _dbContext.Posts.ToListAsync(cancellationToken);

            // TODO AutoMapper.
            var mappedPosts = posts.Select(p => new Post
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
            }).ToList();

            return new Response() { Posts = mappedPosts };
        }
    }
}

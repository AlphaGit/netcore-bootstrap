using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.DAL.Models;
using MediatR;

namespace Alpha.Bootstrap.Logic.Features.Posts.Create
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
                Content = request.Content,
                Title = request.Title,
            };

            _dbContext.Posts.Add(inPost);
            await _dbContext.SaveChangesAsync(cancellationToken);

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

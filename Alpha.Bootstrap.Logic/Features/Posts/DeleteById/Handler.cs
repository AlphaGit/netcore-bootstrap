using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.DeleteById
{
    public class Handler : AsyncRequestHandler<Request>
    {
        private readonly BlogDbContext _dbContext;

        public Handler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(Request request, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (post == null) return;

            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

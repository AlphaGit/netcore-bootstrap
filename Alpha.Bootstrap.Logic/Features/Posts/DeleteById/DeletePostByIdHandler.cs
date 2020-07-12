using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.DeleteById
{
    public class DeletePostByIdHandler : AsyncRequestHandler<DeletePostByIdRequest>
    {
        private readonly BlogDbContext _dbContext;

        public DeletePostByIdHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(DeletePostByIdRequest deletePostByIdRequest, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Id == deletePostByIdRequest.Id, cancellationToken);
            if (post == null) return;

            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

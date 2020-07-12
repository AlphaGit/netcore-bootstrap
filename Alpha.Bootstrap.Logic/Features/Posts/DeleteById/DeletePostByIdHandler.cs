using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.Logic.Models.Errors;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.DeleteById
{
    public class DeletePostByIdHandler : IRequestHandler<DeletePostByIdRequest, Result>
    {
        private readonly BlogDbContext _dbContext;

        public DeletePostByIdHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(DeletePostByIdRequest deletePostByIdRequest, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Id == deletePostByIdRequest.Id, cancellationToken);
            if (post == null) return Result.Fail(new ResourceNotFoundError());

            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

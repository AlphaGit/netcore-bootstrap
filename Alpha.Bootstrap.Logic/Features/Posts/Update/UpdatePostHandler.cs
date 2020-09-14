using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.DAL.Models;
using Alpha.Bootstrap.Logic.Models.Errors;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.Update
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostRequest, Result<Models.Post>>
    {
        private readonly BlogDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdatePostHandler(BlogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<Models.Post>> Handle(UpdatePostRequest updatePostRequest, CancellationToken cancellationToken)
        {
            var postExists = await _dbContext.Posts.AnyAsync(p => p.Id == updatePostRequest.Post.Id, cancellationToken);
            if (!postExists)
                return Result.Fail<Models.Post>(new ResourceNotFoundError());

            var inPost = _mapper.Map<Post>(updatePostRequest.Post);

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

            var outPost = _mapper.Map<Models.Post>(inPost);

            return Result.Ok(outPost);
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetById
{
    public class GetPostByIdHandler : IRequestHandler<GetPostByIdRequest, GetPostByIdResponse>
    {
        private readonly BlogDbContext _dbContext;

        public GetPostByIdHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetPostByIdResponse> Handle(GetPostByIdRequest getPostByIdRequest, CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(p => p.Id == getPostByIdRequest.Id, cancellationToken);

            // TODO AutoMapper.
            var mappedPost = new Post
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
            };

            return new GetPostByIdResponse() { Post = mappedPost };
        }
    }
}

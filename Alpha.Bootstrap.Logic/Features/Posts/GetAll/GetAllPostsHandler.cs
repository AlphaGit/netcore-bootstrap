using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.Logic.Models;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetAll
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPostsRequest, Result<ICollection<Post>>>
    {
        private readonly BlogDbContext _dbContext;

        public GetAllPostsHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ICollection<Post>>> Handle(GetAllPostsRequest getAllPostsRequest, CancellationToken cancellationToken)
        {
            var posts = await _dbContext.Posts.ToListAsync(cancellationToken);

            // TODO AutoMapper.
            var mappedPosts = posts.Select(p => new Post
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
            }).ToList();

            return Result.Ok<ICollection<Post>>(mappedPosts);
        }
    }
}

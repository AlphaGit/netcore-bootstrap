using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.DAL;
using Alpha.Bootstrap.Logic.Models;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpha.Bootstrap.Logic.Features.Posts.GetAll
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPostsRequest, Result<ICollection<Post>>>
    {
        private readonly BlogDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllPostsHandler(BlogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<ICollection<Post>>> Handle(GetAllPostsRequest getAllPostsRequest, CancellationToken cancellationToken)
        {
            var posts = await _dbContext.Posts.ToListAsync(cancellationToken);

            var mappedPosts = _mapper.Map<List<Post>>(posts);

            return Result.Ok<ICollection<Post>>(mappedPosts);
        }
    }
}

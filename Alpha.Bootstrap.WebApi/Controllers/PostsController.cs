using System;
using System.Linq;
using System.Threading.Tasks;
using Alpha.Bootstrap.Logic.Models;
using Alpha.Bootstrap.WebApi.Dtos.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Features = Alpha.Bootstrap.Logic.Features;

namespace Alpha.Bootstrap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllPostsResponse>> Get()
        {
            var command = new Features.Posts.GetAll.Request();
            var response = await _mediator.Send(command);

            // TODO AutoMapper.
            var mappedPosts = response.Posts.Select(MapToPostDto).ToList();

            return new GetAllPostsResponse()
            {
                Posts = mappedPosts
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPostByIdResponse>> GetById(Guid id)
        {
            var command = new Features.Posts.GetById.Request() { Id = id };
            var response = await _mediator.Send(command);

            // TODO AutoMapper.
            var post = response.Post;
            var mappedPost = MapToPostDto(post);

            return new GetPostByIdResponse()
            {
                Post = mappedPost
            };
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            // TODO
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // TODO
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // TODO
        }

        private static PostDto MapToPostDto(Post post)
        {
            return post == null ? null : new PostDto()
            {
                Content = post.Content,
                Id = post.Id,
                Title = post.Title,
            };
        }
    }
}

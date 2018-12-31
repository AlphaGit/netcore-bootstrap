using System;
using System.Linq;
using System.Threading.Tasks;
using Alpha.Bootstrap.Logic.Models;
using Alpha.Bootstrap.WebApi.Dtos.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Features = Alpha.Bootstrap.Logic.Features;

namespace Alpha.Bootstrap.WebApi.Controllers
{
    [Route("api/posts")]
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

        [HttpGet("{id}", Name = "GetPostById")]
        public async Task<ActionResult<GetPostByIdResponse>> GetById(Guid id)
        {
            var command = new Features.Posts.GetById.Request() { Id = id };
            var response = await _mediator.Send(command);

            // TODO AutoMapper.
            var mappedPost = MapToPostDto(response.Post);

            return new GetPostByIdResponse()
            {
                Post = mappedPost
            };
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostRequest postCreateDto)
        {
            var command = new Features.Posts.Create.Request()
            {
                Content = postCreateDto.Content,
                Title = postCreateDto.Title,
            };
            var response = await _mediator.Send(command);

            // TODO AutoMapper.
            var mappedPost = MapToPostDto(response.Post);

            return CreatedAtRoute("GetPostById", new { mappedPost.Id }, mappedPost);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdatePostRequest postUpdateDto)
        {
            var command = new Features.Posts.Update.Request()
            {
                Post = new Post()
                {
                    Content = postUpdateDto.Content,
                    Id = id,
                    Title = postUpdateDto.Title,
                }
            };
            await _mediator.Send(command);

            var routeUrl = Url.RouteUrl(new UrlRouteContext()
            {
                Host = Request.Host.Host,
                Protocol = Request.Scheme,
                RouteName = "GetPostById",
                Values = new { Id = id }
            });

            Response.Headers.Add("Location", routeUrl);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var command = new Features.Posts.DeleteById.Request() { Id = id };
            await _mediator.Send(command);

            return NoContent();
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

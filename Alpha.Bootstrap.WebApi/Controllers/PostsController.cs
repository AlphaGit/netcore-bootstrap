﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Alpha.Bootstrap.Logic.Models;
using Alpha.Bootstrap.WebApi.Dtos.v1.Posts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation("Gets all posts.")]
        public async Task<ActionResult<GetAllPostsResponse>> Get()
        {
            var command = new Features.Posts.GetAll.GetAllPostsRequest();
            var response = await _mediator.Send(command);

            if (response.IsFailed)
                return WebApiResponses.GetErrorResponse(response);

            // TODO AutoMapper.
            var mappedPosts = response.Value.Select(MapToPostDto).ToList();

            return new GetAllPostsResponse()
            {
                Posts = mappedPosts
            };
        }

        [HttpGet("{id}", Name = "GetPostById")]
        [SwaggerOperation("Gets a post by its identifier.")]
        public async Task<ActionResult<GetPostByIdResponse>> GetById(Guid id)
        {
            var command = new Features.Posts.GetById.GetPostByIdRequest() { Id = id };
            var response = await _mediator.Send(command);

            if (response.IsFailed)
                return WebApiResponses.GetErrorResponse(response);

            // TODO AutoMapper.
            var mappedPost = MapToPostDto(response.Value);

            return new GetPostByIdResponse()
            {
                Post = mappedPost
            };
        }

        [HttpPost]
        [SwaggerOperation("Creates a new post.")]
        public async Task<ActionResult> CreatePost([FromBody] CreatePostRequest postCreateDto)
        {
            var command = new Features.Posts.Create.CreatePostRequest()
            {
                Content = postCreateDto.Content,
                Title = postCreateDto.Title,
            };
            var response = await _mediator.Send(command);

            if (response.IsFailed)
                return WebApiResponses.GetErrorResponse(response);

            // TODO AutoMapper.
            var mappedPost = MapToPostDto(response.Value);

            return CreatedAtRoute("GetPostById", new { mappedPost.Id }, mappedPost);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Updates an existing post.")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdatePostRequest postUpdateDto)
        {
            var command = new Features.Posts.Update.UpdatePostRequest()
            {
                Post = new Post()
                {
                    Content = postUpdateDto.Content,
                    Id = id,
                    Title = postUpdateDto.Title,
                }
            };
            var response = await _mediator.Send(command);

            if (response.IsFailed)
                return WebApiResponses.GetErrorResponse(response);

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
        [SwaggerOperation("Deletes a post.")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var command = new Features.Posts.DeleteById.DeletePostByIdRequest() { Id = id };
            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return WebApiResponses.GetErrorResponse(result);

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

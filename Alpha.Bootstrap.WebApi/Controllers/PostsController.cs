using System.Linq;
using System.Threading.Tasks;
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
            var mappedPosts = response.Posts.Select(p =>
                new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                }).ToList();

            return new GetAllPostsResponse()
            {
                Posts = mappedPosts
            };
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
    }
}

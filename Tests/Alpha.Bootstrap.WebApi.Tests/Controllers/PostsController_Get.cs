using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.Logic.Features.Posts.GetAll;
using Alpha.Bootstrap.Logic.Models;
using Alpha.Bootstrap.WebApi.Controllers;
using Alpha.Bootstrap.WebApi.Tests.Fakers;
using MediatR;
using Moq;
using Xunit;

namespace Alpha.Bootstrap.WebApi.Tests.Controllers
{
    public class PostsController_Get
    {
        private readonly LogicDtoFaker _logicDtoFaker;

        private readonly PostsController _controller;

        private readonly Mock<IMediator> _mediatorMock;

        public PostsController_Get()
        {
            _logicDtoFaker = new LogicDtoFaker();

            _mediatorMock = new Mock<IMediator>();

            _controller = new PostsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task ReturnsAllPosts()
        {
            // Arrange.
            var expectedPosts = _logicDtoFaker.Generate(2);
            SetupMediatorResponse(expectedPosts);

            // Act.
            var response = await _controller.Get();

            Assert.NotNull(response);

            var posts = response.Value.Posts;
            Assert.NotNull(posts);
            Assert.NotEmpty(posts);

            foreach (var expectedPost in expectedPosts)
            {
                var actualPost = posts.Single(p => p.Id == expectedPost.Id);
                Assert.Equal(expectedPost.Content, actualPost.Content);
                Assert.Equal(expectedPost.Content, actualPost.Content);
            }

            VerifyMediatorCalled();
        }

        private void VerifyMediatorCalled()
        {
            _mediatorMock.Verify(mock =>
                mock.Send(It.IsAny<Request>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        private void SetupMediatorResponse(ICollection<Post> posts)
        {
            _mediatorMock
                .Setup(mock => mock.Send(It.IsAny<Request>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Response()
                {
                    Posts = posts
                });
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Alpha.Bootstrap.Logic.Features.Posts.GetById;
using Alpha.Bootstrap.Logic.Models;
using Alpha.Bootstrap.WebApi.Controllers;
using Alpha.Bootstrap.WebApi.Tests.Fakers;
using FluentResults;
using MediatR;
using Moq;
using Xunit;

namespace Alpha.Bootstrap.WebApi.Tests.Controllers
{
    public class PostsController_GetById
    {
        private readonly LogicDtoFaker _logicDtoFaker;

        private readonly PostsController _controller;

        private readonly Mock<IMediator> _mediatorMock;

        public PostsController_GetById()
        {
            _logicDtoFaker = new LogicDtoFaker();

            _mediatorMock = new Mock<IMediator>();

            _controller = new PostsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task ReturnsFoundPost()
        {
            // Arrange.
            var expectedPost = _logicDtoFaker.Generate();
            SetupMediatorResponse(expectedPost);

            // Act.
            var response = await _controller.GetById(expectedPost.Id);

            // Assert.
            Assert.NotNull(response);

            var actualPost = response.Value.Post;
            Assert.NotNull(actualPost);
            Assert.Equal(expectedPost.Id, actualPost.Id);
            Assert.Equal(expectedPost.Content, actualPost.Content);
            Assert.Equal(expectedPost.Content, actualPost.Content);

            VerifyMediatorCalled();
        }

        [Fact]
        public async Task ReturnsNotFound()
        {
            // Arrange.
            SetupMediatorResponse(null);

            // Act.
            var response = await _controller.GetById(Guid.NewGuid());

            // Assert.
            Assert.Null(response.Value.Post);
        }

        private void VerifyMediatorCalled()
        {
            _mediatorMock.Verify(mock =>
                mock.Send(It.IsAny<GetPostByIdRequest>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        private void SetupMediatorResponse(Post post)
        {
            _mediatorMock
                .Setup(mock => mock.Send(It.IsAny<GetPostByIdRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(post));
        }
    }
}

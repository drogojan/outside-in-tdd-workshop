using System;
using System.Collections.Generic;
using Moq;
using OpenChat.Application.Posts;
using static OpenChat.Test.Infrastructure.PostApiModelBuilder;
using Xunit;
using OpenChat.API.Controllers;
using OpenChat.Application.Wall;
using FluentAssertions;

namespace OpenChat.Presentation.UnitTests
{
    public class WallControllerTests
    {
        [Fact]
        public void Return_the_wall_posts()
        {
            var POST1 = APostApiModel().Build();
            var POST2 = APostApiModel().Build();
            IEnumerable<PostApiModel> POSTS = new List<PostApiModel> { POST1, POST2 };
            Mock<IWallService> wallServiceStub = new Mock<IWallService>();
            Guid USER_ID = Guid.NewGuid();
            wallServiceStub.Setup(m => m.GetWallPosts(USER_ID)).Returns(POSTS);

            var sut = new WallController(wallServiceStub.Object);

            var posts = sut.WallPosts(USER_ID);

            posts.Should().BeEquivalentTo(POSTS);
        }
    }
}
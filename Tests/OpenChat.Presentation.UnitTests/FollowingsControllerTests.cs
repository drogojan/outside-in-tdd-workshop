using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenChat.API.Controllers;
using OpenChat.Application.Common;
using OpenChat.Application.Followings;
using Xunit;

namespace OpenChat.Presentation.UnitTests
{
    public class FollowingsControllerTests
    {
        FollowingInputModel FOLLOWING = new FollowingInputModel
        {
            FollowerId = Guid.NewGuid(),
            FolloweeId = Guid.NewGuid()
        };

        [Fact]
        public void Create_a_following()
        {
            Mock<IFollowingService> followingServiceMock = new Mock<IFollowingService>();

            var sut = new FollowingsController(followingServiceMock.Object);

            var actionResult = sut.Create(FOLLOWING);

            actionResult.Should().BeAssignableTo<CreatedResult>();
            followingServiceMock.Verify(m => m.CreateFollowing(FOLLOWING));
        }

        [Fact]
        public void Return_an_error_when_creating_a_following_that_already_exists()
        {
            Mock<IFollowingService> followingServiceStub = new Mock<IFollowingService>();
            followingServiceStub.Setup(m => m.CreateFollowing(FOLLOWING)).Throws<FollowingAlreadyExistsException>();

            var sut = new FollowingsController(followingServiceStub.Object);

            var actionResult = (BadRequestObjectResult)sut.Create(FOLLOWING);
            actionResult.Should().NotBeNull();

            var apiError = (ApiError)actionResult.Value;
            apiError.Should().NotBeNull();
            apiError.Message.Should().Be("Following already exists");
        }
    }
}
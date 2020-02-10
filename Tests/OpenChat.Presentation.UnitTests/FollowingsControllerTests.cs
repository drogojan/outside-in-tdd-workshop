using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenChat.API.Controllers;
using OpenChat.Application.Common;
using OpenChat.Application.Followings;
using OpenChat.Application.Users;
using static OpenChat.Test.Infrastructure.Builders.UserApiModelBuilder;
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

        [Fact]
        public void Returns_the_users_followed_by_a_user()
        {
            Guid CHARLIE_ID = Guid.NewGuid();
            var ALICE = AUserApiModel().WithUsername("Alice").Build();
            var JOHN = AUserApiModel().WithUsername("John").Build();
            IEnumerable<UserApiModel> FOLLOWED_USERS = new List<UserApiModel> { ALICE, JOHN };

            Mock<IFollowingService> followingServiceStub = new Mock<IFollowingService>();
            followingServiceStub.Setup(m => m.UsersFollowedBy(CHARLIE_ID)).Returns(FOLLOWED_USERS);

            var sut = new FollowingsController(followingServiceStub.Object);
            var followees = sut.GetFollowees(CHARLIE_ID);

            followees.Should().BeEquivalentTo(FOLLOWED_USERS);
        }
    }
}
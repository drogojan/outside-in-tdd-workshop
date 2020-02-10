using Moq;
using OpenChat.Application.Followings;
using OpenChat.Domain.Entities;
using static OpenChat.Test.Infrastructure.Builders.FollowingInputModelBuilder;
using static OpenChat.Test.Infrastructure.Builders.UserBuilder;
using Xunit;
using System;
using FluentAssertions;
using System.Collections.Generic;

namespace OpenChat.Application.UnitTests
{
    public class FollowingServiceTests
    {
        FollowingInputModel FOLLOWING = AFollowingInputModel().Build();

        [Fact]
        public void Create_a_following()
        {
            Mock<IFollowingRepository> followingRepositoryMock = new Mock<IFollowingRepository>();

            var sut = new FollowingService(followingRepositoryMock.Object);
            sut.CreateFollowing(FOLLOWING);

            followingRepositoryMock.Verify(m => m.Add(
                It.Is<Following>(f =>
                    f.FollowerId == FOLLOWING.FollowerId
                    && f.FolloweeId == FOLLOWING.FolloweeId)));
        }

        [Fact]
        public void Throws_FollowingAlreadyExistsException_when_creating_a_following_that_already_exists()
        {
            Mock<IFollowingRepository> followingRepositoryStub = new Mock<IFollowingRepository>();
            followingRepositoryStub.Setup(m => m.IsFollowingRegistered(It.IsAny<Following>())).Returns(true);

            var sut = new FollowingService(followingRepositoryStub.Object);
            Action action = () => sut.CreateFollowing(FOLLOWING);

            action.Should().Throw<FollowingAlreadyExistsException>().WithMessage("Following already exists");
        }

        [Fact]
        public void Return_the_users_followed_by_a_user()
        {
            Guid CHARLIE_ID = Guid.NewGuid();
            var ALICE = AUser().WithUsername("Alice").Build();
            var JOHN = AUser().WithUsername("John").Build();
            IEnumerable<User> FOLLOWEES = new List<User> { ALICE, JOHN };

            Mock<IFollowingRepository> followingRepositoryStub = new Mock<IFollowingRepository>();
            followingRepositoryStub.Setup(m => m.UsersFollowedBy(It.IsAny<Guid>())).Returns(FOLLOWEES);

            var sut = new FollowingService(followingRepositoryStub.Object);
            var followees = sut.UsersFollowedBy(CHARLIE_ID);

            FOLLOWEES.Should().BeEquivalentTo(followees);
        }
    }
}
using System;
using Moq;
using OpenChat.API.Controllers;
using OpenChat.Application.Followings;
using Xunit;

namespace OpenChat.Presentation.UnitTests
{
    public class FollowingsControllerTests
    {
        [Fact]
        public void Create_a_following()
        {
            Mock<IFollowingService> followingServiceMock = new Mock<IFollowingService>();
            FollowingInputModel FOLLOWING = new FollowingInputModel
            {
                FollowerId = Guid.NewGuid(),
                FolloweeId = Guid.NewGuid()
            };

            var sut = new FollowingsController(followingServiceMock.Object);
            sut.Create(FOLLOWING);

            followingServiceMock.Verify(m => m.CreateFollowing(FOLLOWING));
        }
    }
}
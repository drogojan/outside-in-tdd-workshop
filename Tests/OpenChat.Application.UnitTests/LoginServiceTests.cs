using System;
using FluentAssertions;
using Moq;
using OpenChat.Application.Users;
using Xunit;

namespace OpenChat.Application.UnitTests
{
    public class LoginServiceTests
    {
        private static readonly Guid USER_ID = Guid.Parse("04cec3f7-87fa-49b2-80a5-a08f0c7e02e7");
        private readonly UserCredentials USER_CREDENTIALS = new UserCredentials { Username = "Alice", Password = "alice123" };
        private readonly LoggedInUser LOGGED_IN_USER = new LoggedInUser { Id = USER_ID, Username = "Alice", About = "About Alice" };

        [Fact]
        public void Log_in_a_user()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();

            var sut = new LoginService(userRepositoryMock.Object);

            var loggedInUser = sut.Login(USER_CREDENTIALS);

            userRepositoryMock.Verify(m => m.UserFor(USER_CREDENTIALS), Times.Once);
        }

        [Fact]
        public void Return_the_logged_in_user()
        {
            Mock<IUserRepository> userRepositoryStub = new Mock<IUserRepository>();
            userRepositoryStub.Setup(m => m.UserFor(USER_CREDENTIALS)).Returns(LOGGED_IN_USER);

            var sut = new LoginService(userRepositoryStub.Object);

            var actualLoggedInUser = sut.Login(USER_CREDENTIALS);

            actualLoggedInUser.Id.Should().Be(LOGGED_IN_USER.Id);
            actualLoggedInUser.Username.Should().Be(LOGGED_IN_USER.Username);
            actualLoggedInUser.About.Should().Be(LOGGED_IN_USER.About);
        }
    }
}
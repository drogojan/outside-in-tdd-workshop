using System;
using FluentAssertions;
using Moq;
using OpenChat.Application.Users;
using OpenChat.Domain.Entities;
using Xunit;

namespace OpenChat.Application.UnitTests
{
    public class LoginServiceTests
    {
        private static readonly Guid USER_ID = Guid.Parse("04cec3f7-87fa-49b2-80a5-a08f0c7e02e7");
        private readonly UserCredentials USER_CREDENTIALS = new UserCredentials { Username = "Alice", Password = "alice123" };
        private readonly User USER = new User { Id = USER_ID, Username = "Alice", Password = "alice123", About = "About Alice" };

        [Fact]
        public void Return_the_logged_in_user()
        {
            Mock<IUserRepository> userRepositoryStub = new Mock<IUserRepository>();
            userRepositoryStub.Setup(m => m.UserFor(USER_CREDENTIALS)).Returns(USER);

            var sut = new LoginService(userRepositoryStub.Object);

            var actualLoggedInUser = sut.Login(USER_CREDENTIALS);

            USER.Should().BeEquivalentTo(actualLoggedInUser);
        }

        [Fact]
        public void Throw_InvalidCredentialsException_when_crednetials_are_invalid()
        {
            Mock<IUserRepository> userRepositoryStub = new Mock<IUserRepository>();
            userRepositoryStub.Setup(m => m.UserFor(USER_CREDENTIALS)).Returns((User)null);

            var sut = new LoginService(userRepositoryStub.Object);

            Action action = () => sut.Login(USER_CREDENTIALS);

            action.Should().Throw<InvalidCredentialsException>().WithMessage("Invalid credentials");
        }
    }
}
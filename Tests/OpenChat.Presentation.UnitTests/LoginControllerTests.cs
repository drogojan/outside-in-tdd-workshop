using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenChat.API.Controllers;
using OpenChat.Application.Common;
using OpenChat.Application.Users;
using Xunit;

namespace OpenChat.Presentation.UnitTests
{
    public class LoginControllerTests
    {
        private static readonly Guid USER_ID = Guid.Parse("04cec3f7-87fa-49b2-80a5-a08f0c7e02e7");
        private readonly UserCredentials USER_CREDENTIALS = new UserCredentials { Username = "Alice", Password = "alice123" };
        private readonly LoggedInUser LOGGED_IN_USER = new LoggedInUser { Id = USER_ID, Username = "Alice", About = "About Alice" };

        [Fact]
        public void Logs_in_a_user()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();

            var sut = new LoginController(userServiceMock.Object);
            sut.Post(USER_CREDENTIALS);

            userServiceMock.Verify(m => m.Login(USER_CREDENTIALS), Times.Once);
        }

        [Fact]
        public void Returns_the_logged_in_user()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.Login(USER_CREDENTIALS)).Returns(LOGGED_IN_USER);

            var sut = new LoginController(userServiceMock.Object);

            OkObjectResult result = sut.Post(USER_CREDENTIALS) as OkObjectResult;
            result.Should().NotBeNull();

            LoggedInUser actualLoggedInUser = result.Value as LoggedInUser;
            actualLoggedInUser.Should().NotBeNull();

            actualLoggedInUser.Id.Should().Be(LOGGED_IN_USER.Id);
            actualLoggedInUser.Username.Should().Be(LOGGED_IN_USER.Username);
            actualLoggedInUser.About.Should().Be(LOGGED_IN_USER.About);
        }

        [Fact]
        public void Return_an_error_when_credentials_are_invalid()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.Login(USER_CREDENTIALS)).Throws<InvalidCredentialsException>();

            var sut = new LoginController(userServiceMock.Object);
            IActionResult actionResult = sut.Post(USER_CREDENTIALS);
            NotFoundObjectResult notFoundResult = actionResult as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();

            ApiError apiError = notFoundResult.Value as ApiError;
            apiError.Should().NotBeNull();
            apiError.Message.Should().Be("Invalid credentials");
        }
    }
}
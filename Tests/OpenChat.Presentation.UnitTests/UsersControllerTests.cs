using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using OpenChat.API.Controllers;
using OpenChat.Application.Common;
using OpenChat.Application.Users;
using Xunit;

namespace OpenChat.Presentation.UnitTests
{
    public class UsersControllerTests
    {
        private static readonly Guid USER_ID = Guid.Parse("04cec3f7-87fa-49b2-80a5-a08f0c7e02e7");
        private static readonly RegistrationInputModel REGISTRATION_DATA = new RegistrationInputModel { Username = "Alice", Password = "alice123", About = "Something about Alice" };
        private static readonly RegisteredUserApiModel USER = new RegisteredUserApiModel { Id = USER_ID, Username = "Alice", About = "Something about Alice" };

        [Fact]
        public void Create_a_new_user()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();

            var sut = new UsersController(userServiceMock.Object);
            sut.Create(REGISTRATION_DATA);

            userServiceMock.Verify(m => m.CreateUser(REGISTRATION_DATA), Times.Once);
        }

        [Fact]
        public void Return_the_newly_created_user()
        {
            Mock<IUserService> userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(m => m.CreateUser(REGISTRATION_DATA)).Returns(USER);

            var sut = new UsersController(userServiceStub.Object);

            var actionResult = sut.Create(REGISTRATION_DATA);

            var createdResult = actionResult as CreatedResult;
            createdResult.Should().NotBeNull();

            var createdUser = createdResult.Value as RegisteredUserApiModel;
            createdUser.Should().NotBeNull();

            createdUser.Id.Should().Be(USER.Id);
            createdUser.Username.Should().Be(USER.Username);
            createdUser.About.Should().Be(USER.About);
        }

        [Fact]
        public void Return_an_error_when_creating_a_user_with_an_existing_username()
        {
            Mock<IUserService> userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(m => m.CreateUser(REGISTRATION_DATA)).Throws<UsernameAlreadyInUseException>();

            var sut = new UsersController(userServiceStub.Object);

            var actionResult = sut.Create(REGISTRATION_DATA);
            var badRequestResult = actionResult as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();

            var apiError = badRequestResult.Value as ApiError;
            apiError.Should().NotBeNull();
            apiError.Message.Should().Be("Username already in use");
        }
    }
}

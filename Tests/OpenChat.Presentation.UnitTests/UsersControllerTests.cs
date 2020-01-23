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
        private static readonly UserInputModel REGISTRATION_DATA = new UserInputModel { Username = "Alice", Password = "alice123", About = "Something about Alice" };
        private static readonly UserApiModel USER = new UserApiModel { Id = 1, Username = "Alice", About = "Something about Alice" };

        [Fact]
        public void Create_a_new_user()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();

            var sut = new UsersController(userServiceMock.Object);
            sut.Create(REGISTRATION_DATA);

            userServiceMock.Verify(m => m.CreateUser(REGISTRATION_DATA));
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

            var createdUser = createdResult.Value as UserApiModel;
            createdUser.Should().NotBeNull();

            createdUser.Id.Should().Be(USER.Id);
            createdUser.Username.Should().Be(USER.Username);
            createdUser.About.Should().Be(USER.About);
        }

        [Fact]
        public void Throws_UsernameAlreadyInUseException_when_creating_a_user_with_an_existing_username()
        {
            Mock<IUserService> userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(m => m.CreateUser(REGISTRATION_DATA)).Throws<UsernameAlreadyInUseException>();

            var sut = new UsersController(userServiceStub.Object);

            var actionResult = sut.Create(REGISTRATION_DATA);
            var badRequestResult = actionResult as BadRequestObjectResult;

            var apiError = badRequestResult.Value as ApiError;
            apiError.Message.Should().Be("Username already in use");
        }
    }
}

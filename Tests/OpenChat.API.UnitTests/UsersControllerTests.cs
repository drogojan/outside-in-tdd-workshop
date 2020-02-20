using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenChat.API.Controllers;
using OpenChat.Application.Common;
using OpenChat.Application.Users;
using Xunit;

namespace OpenChat.API.UnitTests
{
    public class UsersControllerTests
    {
        private const string USERNAME = "Alice";
        private const string PASSWORD = "alice123";
        private const string ABOUT = "About Alice";

        private readonly UserRegistration userRegistration = new UserRegistration()
        {
            Username = USERNAME,
            Password = PASSWORD,
            About = ABOUT
        };

        private readonly UserVm userVm = new UserVm
        {
            Id = Guid.NewGuid(),
            Username = USERNAME,
            About = ABOUT
        };

        [Fact]
        public void Create_a_new_user()
        {
            Mock<IUserService> userServiceMock = new Mock<IUserService>();

            var sut = new UsersController(userServiceMock.Object);
            sut.RegisterUser(userRegistration);

            userServiceMock.Verify(
                m => m.CreateUser(userRegistration), Times.Once);
        }

        [Fact]
        public void Return_the_newly_created_user()
        {
            Mock<IUserService> userServiceStub = new Mock<IUserService>();
            userServiceStub
                .Setup(m => m.CreateUser(userRegistration))
                .Returns(userVm);

            var sut = new UsersController(userServiceStub.Object);
            var actionResult = sut.RegisterUser(userRegistration);

            actionResult.Should().BeOfType<CreatedResult>();
            var createdResult = actionResult as CreatedResult;
            createdResult.Value.Should().BeOfType<UserVm>();

            var registeredUser = createdResult.Value as UserVm;
            registeredUser.Should().BeEquivalentTo(userVm);
        }

        [Fact]
        public void Return_an_error_when_trying_to_create_a_user_with_an_existing_username()
        {
            Mock<IUserService> userServiceStub = new Mock<IUserService>();
            userServiceStub
                .Setup(m => m.CreateUser(userRegistration))
                .Throws<UsernameAlreadyInUseException>();

            var sut = new UsersController(userServiceStub.Object);

            var actionResult = sut.RegisterUser(userRegistration);
            actionResult.Should().BeOfType<BadRequestObjectResult>();
            var badRequestObjectResult = actionResult as BadRequestObjectResult;
            badRequestObjectResult.Value.Should().BeOfType<ApiError>();

            var apiError = badRequestObjectResult.Value as ApiError;
            apiError.Message.Should().Be("Username already in use");
        }
    }
}

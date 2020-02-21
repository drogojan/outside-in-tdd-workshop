using System;
using FluentAssertions;
using Moq;
using OpenChat.Application.Users;
using OpenChat.Common;
using OpenChat.Domain;
using Xunit;

namespace OpenChat.Application.UnitTests
{
    public class UserServiceTests
    {
        private const string USERNAME = "Alice";
        private const string PASSWORD = "alice123";
        private const string ABOUT = "About Alice";
        private readonly Guid USER_ID = Guid.NewGuid();


        private readonly UserRegistration userRegistration = new UserRegistration()
        {
            Username = USERNAME,
            Password = PASSWORD,
            About = ABOUT
        };

        [Fact]
        public void Create_a_new_user()
        {
            Mock<IGuidGenerator> guidGeneratorStub = new Mock<IGuidGenerator>();
            guidGeneratorStub.Setup(m => m.Next()).Returns(USER_ID);
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();

            var sut = new UserService(guidGeneratorStub.Object, userRepositoryMock.Object);
            var createdUser = sut.CreateUser(userRegistration);

            userRepositoryMock.Verify(m => m.Add(
                It.Is<User>(u =>
                    u.Id == USER_ID
                    && u.Username == USERNAME
                    && u.Password == PASSWORD
                    && u.About == ABOUT)
            ));

            createdUser.Id.Should().Be(USER_ID);
            createdUser.Username.Should().Be(USERNAME);
            createdUser.About.Should().Be(ABOUT);
        }

        [Fact]
        public void Throws_UsernameAlreadyInUseException_when_trying_to_create_a_user_with_an_existing_username()
        {
            Mock<IGuidGenerator> guidGeneratorStub = new Mock<IGuidGenerator>();
            guidGeneratorStub.Setup(m => m.Next()).Returns(USER_ID);
            Mock<IUserRepository> userRepositoryStub = new Mock<IUserRepository>();
            userRepositoryStub
                .Setup(m => m.IsUsernameTaken(USERNAME))
                .Throws<UsernameAlreadyInUseException>();

            var sut = new UserService(guidGeneratorStub.Object, userRepositoryStub.Object);

            Action action = () => sut.CreateUser(userRegistration);

            action
                .Should()
                .Throw<UsernameAlreadyInUseException>()
                .WithMessage("Username already in use");
        }
    }
}

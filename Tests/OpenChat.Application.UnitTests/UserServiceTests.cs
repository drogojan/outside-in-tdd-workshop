using System;
using Moq;
using Xunit;
using OpenChat.Application.Users;
using OpenChat.Common;
using FluentAssertions;
using OpenChat.Domain.Entities;
using System.Collections.Generic;
using static OpenChat.Test.Infrastructure.Builders.UserBuilder;

namespace OpenChat.Application.UnitTests
{
    public class UserServiceTests
    {
        Guid USER_ID = Guid.Parse("04cec3f7-87fa-49b2-80a5-a08f0c7e02e7");
        private const string USERNAME = "Alice";
        private const string PASSWORD = "alice123";
        private const string ABOUT = "Something about Alice";
        private readonly RegistrationInputModel REGISTRATION_DATA = new RegistrationInputModel { Username = USERNAME, Password = PASSWORD, About = ABOUT };

        [Fact]
        public void Create_a_user()
        {
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            IGuidGenerator guidGeneratorStub = Mock.Of<IGuidGenerator>(g => g.Next() == USER_ID);

            var sut = new UserService(guidGeneratorStub, userRepositoryMock.Object);
            var createdUser = sut.CreateUser(REGISTRATION_DATA);

            userRepositoryMock.Verify(m => m.Add(
                It.Is<User>(
                    u => 
                        u.Id == USER_ID
                        && u.Username == USERNAME
                        && u.Password == PASSWORD
                        && u.About == ABOUT
                    )));
            createdUser.Id.Should().Be(USER_ID);
            createdUser.Username.Should().Be(USERNAME);
            createdUser.About.Should().Be(ABOUT);
        }

        [Fact]
        public void Throws_UsernameAlreadyInUseException_when_creating_a_user_with_an_existing_username()
        {
            IUserRepository userRepositoryStub = Mock.Of<IUserRepository>(m => m.IsUsernameTaken(USERNAME) == true);
            IGuidGenerator guidGeneratorStub = Mock.Of<IGuidGenerator>(g => g.Next() == USER_ID);

            var sut = new UserService(guidGeneratorStub, userRepositoryStub);

            Action action = () => sut.CreateUser(REGISTRATION_DATA);

            action.Should().Throw<UsernameAlreadyInUseException>().WithMessage("Username already in use");
        }

        [Fact]
        public void Returns_all_the_users()
        {
            User ALICE = AUser().WithUsername("Alice").Build();
            User JOHN = AUser().WithUsername("John").Build();
            User MARIE = AUser().WithUsername("Marie").Build();
            IEnumerable<User> USERS = new List<User> { ALICE, JOHN, MARIE };
            IUserRepository userRepositoryStub = Mock.Of<IUserRepository>(m => m.AllUsers() == USERS);
            IGuidGenerator guidGeneratorDummy = Mock.Of<IGuidGenerator>();

            var sut = new UserService(guidGeneratorDummy, userRepositoryStub);

            IEnumerable<UserApiModel> allUsers = sut.AllUsers();

            USERS.Should().BeEquivalentTo(allUsers);
        }
    }
}

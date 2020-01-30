using System;
using Xunit;
using OpenChat.Domain.Entities;
using OpenChat.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OpenChat.Application.Users;

namespace OpenChat.Persistence.UnitTests
{
    public class UserRepositoryTests
    {
        static Guid ALICE_ID = Guid.Parse("4d1063a6-94ad-4549-876a-77ee113df978");
        static Guid CHARLIE_ID = Guid.Parse("b948f394-ffde-44da-97f7-9e6cb0b8dfe2");

        User ALICE = new User { Id = ALICE_ID, Username = "Alice", Password = "Alis", About = "About Alice" };
        User CHARLIE = new User { Id = CHARLIE_ID, Username = "Charlie", Password = "Charli", About = "About Charlie" };

        [Fact]
        public void Inform_when_a_username_is_already_taken()
        {
            OpenChatDbContext dbContext = CreateInMemoryDbContext();

            UserRepository sut = new UserRepository(dbContext);

            sut.Add(ALICE);

            sut.IsUsernameTaken(ALICE.Username).Should().BeTrue();
            sut.IsUsernameTaken(CHARLIE.Username).Should().BeFalse();
        }

        [Fact]
        public void Return_user_matching_valid_credentials()
        {
            OpenChatDbContext dbContext = CreateInMemoryDbContext();
            UserCredentials ALICE_CREDENTIALS = new UserCredentials { Username = ALICE.Username, Password = ALICE.Password };
            UserCredentials CHARLIE_CREDENTIALS = new UserCredentials { Username = CHARLIE.Username, Password = CHARLIE.Password };
            UserCredentials UNKNOWN_CREDENTIALS = new UserCredentials { Username = "unknown", Password = "unknown" };

            UserRepository sut = new UserRepository(dbContext);

            sut.Add(ALICE);
            sut.Add(CHARLIE);

            var alice = sut.UserFor(ALICE_CREDENTIALS);
            var charlie = sut.UserFor(CHARLIE_CREDENTIALS);
            var unknown = sut.UserFor(UNKNOWN_CREDENTIALS);

            ALICE.Should().BeEquivalentTo(alice, options => options.ExcludingMissingMembers());
            CHARLIE.Should().BeEquivalentTo(charlie, options=> options.ExcludingMissingMembers());
            unknown.Should().BeNull();
        }

        private OpenChatDbContext CreateInMemoryDbContext()
        {
            DbContextOptionsBuilder<OpenChatDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<OpenChatDbContext>();

            dbContextOptionsBuilder.UseInMemoryDatabase("OpenChat-InMemory-DB");

            return new OpenChatDbContext(dbContextOptionsBuilder.Options);
        }
    }
}

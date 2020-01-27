using System;
using Xunit;
using OpenChat.Domain.Entities;
using OpenChat.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace OpenChat.Persistence.UnitTests
{
    public class UserRepositoryTests
    {

        [Fact]
        public void Inform_when_a_username_is_already_taken()
        {
            Guid ALICE_ID = Guid.Parse("4d1063a6-94ad-4549-876a-77ee113df978");
            Guid CHARLIE_ID = Guid.Parse("b948f394-ffde-44da-97f7-9e6cb0b8dfe2");
            User ALICE = new User { Id = ALICE_ID, Username = "Alice", Password = "Alis", About = "About Alice" };
            User CHARLIE = new User { Id = CHARLIE_ID, Username = "Charlie", Password = "Charli", About = "About Charlie" };

            OpenChatDbContext dbContext = CreateInMemoryDbContext();

            UserRepository sut = new UserRepository(dbContext);

            sut.Add(ALICE);

            sut.IsUsernameTaken(ALICE.Username).Should().BeTrue();
            sut.IsUsernameTaken(CHARLIE.Username).Should().BeFalse();
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

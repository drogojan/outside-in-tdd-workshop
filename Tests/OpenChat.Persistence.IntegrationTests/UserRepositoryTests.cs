using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OpenChat.Domain;
using Xunit;

namespace OpenChat.Persistence.IntegrationTests
{
    public class UserRepositoryTests
    {
        [Fact]
        public void Inform_when_a_username_is_already_taken()
        {
            DbContextOptionsBuilder<OpenChatDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<OpenChatDbContext>();
            dbContextOptionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=OpenChatDBRehearsal;Trusted_Connection=True");
            dbContextOptionsBuilder.EnableSensitiveDataLogging();

            var dbContextOptions = dbContextOptionsBuilder.Options;
            var dbContext = new OpenChatDbContext(dbContextOptions);
            dbContext.Database.Migrate();

            var sut = new UserRepository(dbContext);

            var john = new User { Id = Guid.NewGuid(), Username = "JOHN", Password = "john", About = "About John"};
            var marie = new User { Id = Guid.NewGuid(), Username = "MARIE", Password = "marie", About = "About Marie"};
            sut.Add(john);

            sut.IsUsernameTaken(john.Username).Should().BeTrue();
            sut.IsUsernameTaken(marie.Username).Should().BeFalse();
        }
    }
}

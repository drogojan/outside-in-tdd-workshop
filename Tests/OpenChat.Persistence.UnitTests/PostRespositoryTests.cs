using System;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OpenChat.Domain.Entities;
using static OpenChat.Test.Infrastructure.Builders.PostBuilder;
using static OpenChat.Test.Infrastructure.Builders.UserBuilder;
using Xunit;

namespace OpenChat.Persistence.UnitTests
{
    public class PostRespositoryTests
    {
        // TODO - implement more functionality to test the command Create(Post)
        // method through a query e.g. PostsByUser(userId)
        User ALICE = AUser().WithUsername("Alice").Build();
        User CHARLIE = AUser().WithUsername("Charlie").Build();

        [Fact]
        public void Return_the_posts_for_a_given_user_in_reverse_chronological_order()
        {
            OpenChatDbContext dbContext = CreateInMemoryDbContext();
            var sut = new PostRepository(dbContext);

            Post ALICE_POST_1 = APost().WithUserId(ALICE.Id).WithText("my first post").WithDateTime(new DateTime(2020, 2, 25, 9, 0, 0)).Build();
            sut.Add(ALICE_POST_1);
            Post CHARLIE_POST_1 = APost().WithUserId(CHARLIE.Id).Build();
            sut.Add(CHARLIE_POST_1);
            Post ALICE_POST_2 = APost().WithUserId(ALICE.Id).WithText("my second post").WithDateTime(new DateTime(2020, 2, 25, 10, 0, 0)).Build();
            sut.Add(ALICE_POST_2);

            var posts = sut.PostsBy(ALICE.Id);

            posts.Should().ContainInOrder(ALICE_POST_2, ALICE_POST_1);
        }

        private OpenChatDbContext CreateInMemoryDbContext()
        {
            DbContextOptionsBuilder<OpenChatDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<OpenChatDbContext>();

            dbContextOptionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            return new OpenChatDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
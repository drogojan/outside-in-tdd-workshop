using System;
using Microsoft.EntityFrameworkCore;
using static OpenChat.Test.Infrastructure.Builders.UserBuilder;
using static OpenChat.Test.Infrastructure.Builders.FollowingBuilder;
using Xunit;
using OpenChat.Domain.Entities;
using FluentAssertions;

namespace OpenChat.Persistence.UnitTests
{
    public class FollowingRepositoryTests
    {
        [Fact]
        public void Inform_when_a_following_is_already_registered()
        {
            var CHARLIE = AUser().WithUsername("CHARLIE").Build();
            var ALICE = AUser().WithUsername("ALICE").Build();
            Following CHARLIE_FOLLOWING_ALICE = AFollowing().WithFollowerId(CHARLIE.Id).WithFolloweeId(ALICE.Id).Build();

            var dbContext = CreateInMemoryDbContext();

            UserRepository userRepository = new UserRepository(dbContext);
            userRepository.Add(CHARLIE);
            userRepository.Add(ALICE);

            var sut = new FollowingRepository(dbContext);

            sut.IsFollowingRegistered(CHARLIE_FOLLOWING_ALICE).Should().BeFalse();

            sut.Add(CHARLIE_FOLLOWING_ALICE);

            sut.IsFollowingRegistered(CHARLIE_FOLLOWING_ALICE).Should().BeTrue();
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
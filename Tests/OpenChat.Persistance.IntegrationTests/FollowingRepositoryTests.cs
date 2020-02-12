using System;
using Microsoft.EntityFrameworkCore;
using static OpenChat.Test.Infrastructure.Builders.UserBuilder;
using static OpenChat.Test.Infrastructure.Builders.FollowingBuilder;
using Xunit;
using OpenChat.Domain.Entities;
using FluentAssertions;
using TestSupport.EfHelpers;
using Xunit.Abstractions;
using OpenChat.Persistence;

namespace OpenChat.Persistance.IntegrationTests
{
    public class FollowingRepositoryTests : IntegrationTests
    {
        public FollowingRepositoryTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public void Inform_when_a_following_is_already_registered()
        {
            var CHARLIE = AUser().WithUsername("CHARLIE").Build();
            var ALICE = AUser().WithUsername("ALICE").Build();
            Following CHARLIE_FOLLOWING_ALICE = AFollowing().WithFollowerId(CHARLIE.Id).WithFolloweeId(ALICE.Id).Build();

            var dbContext = DbContext;

            UserRepository userRepository = new UserRepository(dbContext);
            userRepository.Add(CHARLIE);
            userRepository.Add(ALICE);

            var sut = new FollowingRepository(dbContext);

            sut.IsFollowingRegistered(CHARLIE_FOLLOWING_ALICE).Should().BeFalse();

            sut.Add(CHARLIE_FOLLOWING_ALICE);

            sut.IsFollowingRegistered(CHARLIE_FOLLOWING_ALICE).Should().BeTrue();
        }

        [Fact]
        public void Return_the_users_followed_by_a_user()
        {
            var CHARLIE = AUser().WithUsername("CHARLIE").Build();
            var ALICE = AUser().WithUsername("ALICE").Build();
            var JOHN = AUser().WithUsername("JOHN").Build();
            Following CHARLIE_FOLLOWING_ALICE = AFollowing().WithFollowerId(CHARLIE.Id).WithFolloweeId(ALICE.Id).Build();
            Following CHARLIE_FOLLOWING_JOHN = AFollowing().WithFollowerId(CHARLIE.Id).WithFolloweeId(JOHN.Id).Build();

            var dbContext = DbContext;

            UserRepository userRepository = new UserRepository(dbContext);
            userRepository.Add(CHARLIE);
            userRepository.Add(ALICE);
            userRepository.Add(JOHN);

            var sut = new FollowingRepository(dbContext);
            sut.Add(CHARLIE_FOLLOWING_ALICE);
            sut.Add(CHARLIE_FOLLOWING_JOHN);

            var followees = sut.UsersFollowedBy(CHARLIE.Id);

            followees.Should().BeEquivalentTo(new[] { ALICE, JOHN });
        }
    }
}
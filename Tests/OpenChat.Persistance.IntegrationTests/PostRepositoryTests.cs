using System;
using FluentAssertions;
using OpenChat.Domain.Entities;
using static OpenChat.Test.Infrastructure.Builders.PostBuilder;
using static OpenChat.Test.Infrastructure.Builders.UserBuilder;
using static OpenChat.Test.Infrastructure.Builders.FollowingBuilder;
using Xunit;
using Xunit.Abstractions;
using OpenChat.Persistence;

namespace OpenChat.Persistance.IntegrationTests
{
    public class PostRepositoryTests : IntegrationTests
    {
        public PostRepositoryTests(DbMigrationFixture dbMigrationFixture, ITestOutputHelper testOutputHelper) : base(dbMigrationFixture, testOutputHelper)
        {
        }

        [Fact]
        public void Return_the_posts_for_a_given_user_in_reverse_chronological_order()
        {
            User ALICE = AUser().WithUsername("Alice").Build();
            User CHARLIE = AUser().WithUsername("Charlie").Build();

            UserRepository userRepository = new UserRepository(DbContext);
            userRepository.Add(CHARLIE);
            userRepository.Add(ALICE);

            var sut = new PostRepository(DbContext);

            Post ALICE_POST_1 = APost().WithUserId(ALICE.Id).WithText("my first post").WithDateTime(new DateTime(2020, 2, 25, 9, 0, 0)).Build();
            sut.Add(ALICE_POST_1);
            Post CHARLIE_POST_1 = APost().WithUserId(CHARLIE.Id).Build();
            sut.Add(CHARLIE_POST_1);
            Post ALICE_POST_2 = APost().WithUserId(ALICE.Id).WithText("my second post").WithDateTime(new DateTime(2020, 2, 25, 10, 0, 0)).Build();
            sut.Add(ALICE_POST_2);

            var posts = sut.PostsBy(ALICE.Id);

            posts.Should().ContainInOrder(ALICE_POST_2, ALICE_POST_1);
        }

        [Fact]
        public void Return_the_posts_of_the_user_and_the_posts_of_the_users_he_follows_in_reverse_chronological_order()
        {
            var CHARLIE = AUser().WithUsername("CHARLIE").Build();
            var ALICE = AUser().WithUsername("ALICE").Build();
            var JOHN = AUser().WithUsername("JOHN").Build();
            Following CHARLIE_FOLLOWING_ALICE = AFollowing().WithFollowerId(CHARLIE.Id).WithFolloweeId(ALICE.Id).Build();
            Following CHARLIE_FOLLOWING_JOHN = AFollowing().WithFollowerId(CHARLIE.Id).WithFolloweeId(JOHN.Id).Build();

            UserRepository userRepository = new UserRepository(DbContext);
            userRepository.Add(CHARLIE);
            userRepository.Add(ALICE);
            userRepository.Add(JOHN);

            FollowingRepository followingRepository = new FollowingRepository(DbContext);
            followingRepository.Add(CHARLIE_FOLLOWING_ALICE);
            followingRepository.Add(CHARLIE_FOLLOWING_JOHN);

            var sut = new PostRepository(DbContext);

            Post ALICE_POST_1 = APost().WithUserId(ALICE.Id).WithDateTime(new DateTime(2020, 2, 25, 9, 0, 0)).Build();
            sut.Add(ALICE_POST_1);
            Post CHARLIE_POST_1 = APost().WithUserId(CHARLIE.Id).WithDateTime(new DateTime(2020, 2, 25, 9, 30, 0)).Build();
            sut.Add(CHARLIE_POST_1);
            Post ALICE_POST_2 = APost().WithUserId(ALICE.Id).WithDateTime(new DateTime(2020, 2, 25, 10, 0, 0)).Build();
            sut.Add(ALICE_POST_2);
            Post JOHN_POST_1 = APost().WithUserId(JOHN.Id).WithDateTime(new DateTime(2020, 2, 25, 10, 0, 1)).Build();
            sut.Add(JOHN_POST_1);

            var posts = sut.WallPostsFor(CHARLIE.Id);

            posts.Should().ContainInOrder(JOHN_POST_1, ALICE_POST_2, CHARLIE_POST_1, ALICE_POST_1);
        }
    }
}
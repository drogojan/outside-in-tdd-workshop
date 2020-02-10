using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using OpenChat.API.AcceptanceTests.Models;
using OpenChat.Application.Users;
using Xunit;
using Xunit.Abstractions;
using static OpenChat.API.AcceptanceTests.Builders.UserBuilder;

namespace OpenChat.API.AcceptanceTests
{
    public class FollowingApiTests : ApiTests
    {
        public FollowingApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper) 
            : base(testFixture, testOutputHelper)
        {
        }

        [Fact]
        public async Task Create_a_following()
        {
            var CHARLIE = AUser().WithUsername("Charlie").Build();
            var ALICE = AUser().WithUsername("Alice").Build();

            var charlie = await RegisterUser(CHARLIE);
            var alice = await RegisterUser(ALICE);

            await RegisterFollowing(charlie, alice);
        }

        [Fact]
        public async Task Return_the_users_followed_by_a_user()
        {
            var BOB = AUser().WithUsername("Bob").Build();
            var MARIE = AUser().WithUsername("Marie").Build();
            var JOHN = AUser().WithUsername("John").Build();

            var bob = await RegisterUser(BOB);
            var marie = await RegisterUser(MARIE);
            var john = await RegisterUser(JOHN);

            await RegisterFollowing(bob, marie);
            await RegisterFollowing(bob, john);

            var followeesResponse = await client.GetAsync($"api/followings/{bob.Id}/followees");
            followeesResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var followees = await followeesResponse.Content.ReadAsJsonAsync<IEnumerable<UserApiModel>>();
            followees.Should().BeEquivalentTo(new [] { marie, john });
        }
    }
}
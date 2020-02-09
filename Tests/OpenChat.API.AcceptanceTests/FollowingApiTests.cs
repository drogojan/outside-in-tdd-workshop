using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
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

            var following = new {
                followerId = charlie.Id,
                followeeId = alice.Id
            };

            var createFollowingResponse = await client.PostAsJsonAsync("api/followings", following);
            createFollowingResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
using System.Threading;
using System;
using System.Net;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenChat.API.AcceptanceTests.Models;
using Xunit;
using Xunit.Abstractions;
using static OpenChat.API.AcceptanceTests.Builders.UserBuilder;
using static OpenChat.API.AcceptanceTests.Builders.PostBuilder;
using System.Collections.Generic;

namespace OpenChat.API.AcceptanceTests
{
    public class WallApiTests : ApiTests
    {
        public WallApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper) 
            : base(testFixture, testOutputHelper)
        {
        }

        [Fact]
        public async Task Return_the_posts_of_the_user_and_the_posts_of_the_users_he_follows_in_reverse_chronological_order()
        {
            var BOB = AUser().WithUsername("Bob").Build();
            var MARIE = AUser().WithUsername("Marie").Build();
            var JOHN = AUser().WithUsername("John").Build();

            var bob = await RegisterUser(BOB);
            var marie = await RegisterUser(MARIE);
            var john = await RegisterUser(JOHN);

            await RegisterFollowing(bob, marie);
            await RegisterFollowing(bob, john);

            Post BOB_POST_1 = APost().WithText("Bob's first post").Build();
            Post MARIE_POST_1 = APost().WithText("Maries's first post").Build();
            Post MARIE_POST_2 = APost().WithText("Maries's second post").Build();
            Post JOHN_POST_1 = APost().WithText("John's first post").Build();

            CreatedPost createdPost1 = await CreatePost(BOB_POST_1, bob);
            CreatedPost createdPost2 = await CreatePost(MARIE_POST_1, marie);
            CreatedPost createdPost3 = await CreatePost(MARIE_POST_2, marie);
            CreatedPost createdPost4 = await CreatePost(JOHN_POST_1, john);

            var wallResponse = await client.GetAsync($"api/users/{bob.Id}/wall");
            wallResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var posts = await wallResponse.Content.ReadAsJsonAsync<IEnumerable<CreatedPost>>();
            posts.Should().ContainInOrder(new [] { createdPost4, createdPost3, createdPost2, createdPost1 });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using OpenChat.API.AcceptanceTests.Models;
using Xunit;
using Xunit.Abstractions;
using static OpenChat.API.AcceptanceTests.Builders.PostBuilder;
using static OpenChat.API.AcceptanceTests.Builders.UserBuilder;

namespace OpenChat.API.AcceptanceTests
{
    public class PostApiTests : ApiTests
    {
        public PostApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper)
            : base(testFixture, testOutputHelper)
        {
        }

        [Fact]
        public async Task Create_a_post()
        {
            var JOHN = AUser().WithUsername("john").WithPassword("john123").WithAbout("I like to say hello").Build();
            var john = await RegisterUser(JOHN);
            var post = APost().WithText("Hello World! This is John.").Build();

            await CreatePost(post, john);
        }

        [Fact]
        public async Task Return_the_posts_for_a_user_in_reverse_chronological_order()
        {
            var user = new
            {
                username = "Bob",
                password = "BOB123",
                about = "I like to say howdy"
            };

            var BOB = AUser().WithUsername("Bob").WithPassword("BOB123").WithAbout("I like to say howdy").Build();
            var bob = await RegisterUser(BOB);

            var userId = bob.Id;

            var firstPost = APost().WithText("Hello World! This is Bob.").Build();

            var secondPost = APost().WithText("Hey there! It's me again. Bob. B.O.B.").Build();

            var firstPostCreated = await CreatePost(firstPost, bob);
            var secondPostCreated = await CreatePost(secondPost, bob);

            var postsByUserResponse = await client.GetAsync($"api/users/{userId}/timeline");
            postsByUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var postsByUser = await postsByUserResponse.Content.ReadAsJsonAsync<IEnumerable<CreatedPost>>();
            postsByUser.Should().HaveCount(2);

            postsByUser.Should().ContainInOrder(secondPostCreated, firstPostCreated);
        }
    }
}
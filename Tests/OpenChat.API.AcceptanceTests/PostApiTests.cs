using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using static OpenChat.API.AcceptanceTests.Builders.PostBuilder;
using static OpenChat.API.AcceptanceTests.Builders.UserBuilder;
using Xunit;
using Xunit.Abstractions;
using OpenChat.API.AcceptanceTests.Models;

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
            User JOHN = AUser().WithUsername("john").WithPassword("john123").WithAbout("I like to say hello").Build();
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

            var registeredUserResponse = await client.PostAsJsonAsync("api/users", user);
            registeredUserResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var registeredUser = await registeredUserResponse.Content.ReadAsJsonAsync<JObject>();
            var userId = registeredUser.Value<string>("id");

            var firstPost = new
            {
                text = "Hello World! This is Bob."
            };

            var secondPost = new
            {
                text = "Hey there! It's me again. Bob. B.O.B."
            };

            var createfirstPostReponse = await client.PostAsJsonAsync($"api/users/{userId}/timeline", firstPost);
            createfirstPostReponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var firstCreatedPost = await createfirstPostReponse.Content.ReadAsJsonAsync<JObject>();
            Guid.Parse(firstCreatedPost.Value<string>("postId")).Should().NotBeEmpty();
            firstCreatedPost.Value<string>("userId").Should().Be(userId);
            firstCreatedPost.Value<string>("text").Should().Be(firstPost.text);
            // firstCreatedPost.Value<DateTime>("dateTime") should have specific format

            var secondPostReponseResponse = await client.PostAsJsonAsync($"api/users/{userId}/timeline", secondPost);
            secondPostReponseResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var secondCreatedPost = await secondPostReponseResponse.Content.ReadAsJsonAsync<JObject>();
            Guid.Parse(secondCreatedPost.Value<string>("postId")).Should().NotBeEmpty();
            secondCreatedPost.Value<string>("userId").Should().Be(userId);
            secondCreatedPost.Value<string>("text").Should().Be(secondPost.text);
            // secondCreatedPost.Value<DateTime>("dateTime") should have specific format

            var postsByUserResponse = await client.GetAsync($"api/users/{userId}/timeline");
            postsByUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var postsByUser = await postsByUserResponse.Content.ReadAsJsonAsync<JObject[]>();
            postsByUser.Length.Should().Be(2);

            postsByUser[0].Value<string>("text").Should().Be(secondPost.text);
            postsByUser[1].Value<string>("text").Should().Be(firstPost.text);
        }
    }
}
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace OpenChat.API.AcceptanceTests
{
    public class PostApiTests : IClassFixture<AcceptanceTestFixture>
    {
        private readonly AcceptanceTestFixture testFixture;

        private readonly HttpClient client;

        public PostApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper)
        {
            this.testFixture = testFixture;
            this.testFixture.TestOutputHelper = testOutputHelper;
            this.client = this.testFixture.CreateClient();
        }

        [Fact]
        public async Task Create_a_post()
        {
            var user = new
            {
                username = "John",
                password = "john123",
                about = "I like to say hello"
            };

            var registeredUserResponse = await client.PostAsJsonAsync("api/users", user);
            registeredUserResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var registeredUser = await registeredUserResponse.Content.ReadAsJsonAsync<JObject>();
            var userId = registeredUser.Value<string>("id");

            var post = new
            {
                text = "Hello World! This is John."
            };

            var createPostReponse = await client.PostAsJsonAsync($"api/users/{userId}/timeline", post);

            createPostReponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdPost = await createPostReponse.Content.ReadAsJsonAsync<JObject>();
            Guid.Parse(createdPost.Value<string>("postId")).Should().NotBeEmpty();
            createdPost.Value<string>("userId").Should().Be(userId);
            createdPost.Value<string>("text").Should().Be(post.text);
            // createdPost.Value<DateTime>("dateTime") should have specific format
            // TODO - add CORS and other needed feature to the branch for the workshop
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
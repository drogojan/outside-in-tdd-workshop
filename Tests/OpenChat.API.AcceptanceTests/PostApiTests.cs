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
    }
}
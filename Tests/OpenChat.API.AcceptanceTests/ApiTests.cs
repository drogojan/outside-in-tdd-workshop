using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenChat.API.AcceptanceTests.Models;
using Xunit;
using Xunit.Abstractions;

namespace OpenChat.API.AcceptanceTests
{
    public class ApiTests : IClassFixture<AcceptanceTestFixture>
    {
        private readonly AcceptanceTestFixture testFixture;
        protected readonly HttpClient client;

        public ApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper)
        {
            this.testFixture = testFixture;
            this.testFixture.TestOutputHelper = testOutputHelper;
            this.client = this.testFixture.CreateClient();
        }

        protected async Task<RegisteredUser> RegisterUser(User user)
        {
            var response = await client.PostAsJsonAsync("api/users", user);
            var registeredUser = await response.Content.ReadAsJsonAsync<RegisteredUser>();

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            registeredUser.Should().BeEquivalentTo(user, options =>
                options.Excluding(p => p.Password));
            registeredUser.Id.Should().NotBeEmpty();

            return registeredUser;
        }

        protected async Task RegisterFollowing(RegisteredUser follower, RegisteredUser followee)
        {
            var following = new
            {
                followerId = follower.Id,
                followeeId = followee.Id
            };

            var createFollowingResponse = await client.PostAsJsonAsync("api/followings", following);
            createFollowingResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        protected async Task<CreatedPost> CreatePost(Post post, RegisteredUser forUser)
        {
            var createPostReponse = await client.PostAsJsonAsync($"api/users/{forUser.Id}/timeline", post);

            createPostReponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var content = await createPostReponse.Content.ReadAsStringAsync();
            CreatedPost createdPost = JsonConvert.DeserializeObject<CreatedPost>(content, new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"
            });
            createdPost.PostId.Should().NotBeEmpty();
            createdPost.UserId.Should().Be(forUser.Id);
            createdPost.Text.Should().Be(post.Text);
            // createdPost.DateTime.Should().BeCloseTo(DateTime.UtcNow, 5000);

            return createdPost;
        }
    }
}
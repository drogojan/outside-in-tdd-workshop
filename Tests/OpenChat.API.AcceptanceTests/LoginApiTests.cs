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
    public class LoginApiTests : IClassFixture<AcceptanceTestFixture>
    {
        private readonly AcceptanceTestFixture testFixture;
        private readonly HttpClient client;

        public LoginApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper)
        {
            this.testFixture = testFixture;
            this.testFixture.TestOutputHelper = testOutputHelper;
            this.client = this.testFixture.CreateClient();
        }

        [Fact]
        public async Task Login_a_user()
        {
            var user = new {
                username = "Charlie",
                password = "charli3",
                about = "I like to cook"
            };

            var registeredUserResponse = await client.PostAsJsonAsync("api/users", user);

            var registeredUser = await registeredUserResponse.Content.ReadAsJsonAsync<JObject>();

            var userCredentials = new {
                username = user.username,
                password = user.password
            };

            var loginResponse = await client.PostAsJsonAsync("api/login", userCredentials);

            loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var loggedInUser = await loginResponse.Content.ReadAsJsonAsync<JObject>();
            Guid.Parse(loggedInUser.Value<string>("id")).Should().NotBeEmpty();
            loggedInUser.Value<string>("username").Should().Be(user.username);
            loggedInUser.Value<string>("about").Should().Be(user.about);
        }
    }
}
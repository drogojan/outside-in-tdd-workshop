using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace OpenChat.API.AcceptanceTests
{
    public class RegistrationApiTests : ApiTests
    {
        public RegistrationApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper) : base(testFixture, testOutputHelper)
        {
        }

        [Fact]
        public async Task Register_a_user()
        {
            var user = new
            {
                username = "alice",
                password = "alice123",
                about = "I like to travel"
            };

            var response = await client.PostAsJsonAsync("api/users", user);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var actual = await response.Content.ReadAsJsonAsync<JObject>();
            Guid.Parse(actual.Value<string>("id")).Should().NotBeEmpty();
            actual.Value<string>("username").Should().Be(user.username);
            actual.Value<string>("about").Should().Be(user.about);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace OpenChat.API.AcceptanceTests
{
    public class RegistrationApiTests : IClassFixture<AcceptanceTestFixture>
    {
        private readonly AcceptanceTestFixture testFixture;
        private readonly HttpClient client;

        public RegistrationApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper)
        {
            this.testFixture = testFixture;
            this.testFixture.TestOutputHelper = testOutputHelper;
            this.client = this.testFixture.CreateClient();
        }

        [Fact]
        public async Task Register_a_new_user()
        {
            var user = new {
                username = "Alice",
                password = "alice123",
                about = "I like to travel."
            };

            var response = await client.PostAsJsonAsync("api/users", user);

            var actual = await response.Content.ReadAsJsonAsync<JObject>();

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            actual.Value<Guid>("id").Should().NotBeEmpty();
            actual.Value<string>("username").Should().Be(user.username);
            actual.Value<string>("about").Should().Be(user.about);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using Xunit;

namespace OpenChat.API.AcceptanceTests
{
    public class RegistrationApiTests : IClassFixture<WebApplicationFactory<OpenChat.API.Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public RegistrationApiTests(WebApplicationFactory<OpenChat.API.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Register_a_new_user()
        {
            var client = _factory.CreateClient();

            var user = new {
                username = "Alice",
                password = "alice123",
                about = "I like to travel."
            };

            var response = await client.PostAsJsonAsync("api/users", user);

            var actual = await response.Content.ReadAsJsonAsync<JObject>();

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            actual.Value<int>("id").Should().BePositive();
            actual.Value<string>("username").Should().Be(user.username);
            actual.Value<string>("about").Should().Be(user.about);
        }
    }
}

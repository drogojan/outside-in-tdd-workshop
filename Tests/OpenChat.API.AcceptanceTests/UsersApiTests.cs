using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using static OpenChat.API.AcceptanceTests.Builders.UserBuilder;
using Xunit;
using Xunit.Abstractions;
using OpenChat.API.AcceptanceTests.Models;
using System.Collections.Generic;

namespace OpenChat.API.AcceptanceTests
{
    public class UsersApiTests : IClassFixture<AcceptanceTestFixture>
    {
        private readonly AcceptanceTestFixture testFixture;
        private readonly HttpClient client;

        public UsersApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper)
        {
            this.testFixture = testFixture;
            this.testFixture.TestOutputHelper = testOutputHelper;
            this.client = this.testFixture.CreateClient();
        }

        [Fact]
        public async Task Return_all_users()
        {
            var CHARLIE = AUser().WithUsername("Charlie").Build();
            var ALICE = AUser().WithUsername("Alice").Build();
            var BOB = AUser().WithUsername("Bob").Build();

            var charlie = await RegisterUser(CHARLIE);
            var alice = await RegisterUser(ALICE);
            var bob = await RegisterUser(BOB);

            IEnumerable<RegisteredUser> allUsers = await GetAllUsers();

            allUsers.Should().BeEquivalentTo(new [] { charlie, alice, bob });
        }

        private async Task<IEnumerable<RegisteredUser>> GetAllUsers()
        {
            var response = await client.GetAsync("api/users");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var allUsers = await response.Content.ReadAsJsonAsync<IEnumerable<RegisteredUser>>();

            return allUsers;
        }

        private async Task<RegisteredUser> RegisterUser(User user)
        {
            var response = await client.PostAsJsonAsync("api/users", user);
            var registeredUser = await response.Content.ReadAsJsonAsync<RegisteredUser>();

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            registeredUser.Should().BeEquivalentTo(user, options =>
                options.Excluding(p => p.Password));
            registeredUser.Id.Should().NotBeEmpty();

            return registeredUser;
        }
    }
}
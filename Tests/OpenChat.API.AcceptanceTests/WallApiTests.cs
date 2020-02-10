using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OpenChat.API.AcceptanceTests
{
    public class WallApiTests : ApiTests
    {
        public WallApiTests(AcceptanceTestFixture testFixture, ITestOutputHelper testOutputHelper) : base(testFixture, testOutputHelper)
        {
        }

        [Fact]
        public async Task Return_the_posts_of_the_user_and_the_posts_of_the_users_he_follows()
        {
            var BOB = AUser().WithUsername("Bob").Build();
            var MARIE = AUser().WithUsername("Marie").Build();
            var JOHN = AUser().WithUsername("John").Build();

            var bob = await RegisterUser(BOB);
            var marie = await RegisterUser(MARIE);
            var john = await RegisterUser(JOHN);

            await RegisterFollowing(bob, marie);
            await RegisterFollowing(bob, john);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
    }
}

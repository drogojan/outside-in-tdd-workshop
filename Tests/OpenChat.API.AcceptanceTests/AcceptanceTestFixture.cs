using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace OpenChat.API.AcceptanceTests
{
    public class AcceptanceTestFixture : WebApplicationFactory<OpenChat.API.Startup>
    {
        public ITestOutputHelper TestOutputHelper { get; set; }

        protected override IHostBuilder CreateHostBuilder()
        {
            var hostBuilder = base.CreateHostBuilder();
            hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddXUnit(TestOutputHelper);
            });

            return hostBuilder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            builder.ConfigureAppConfiguration(configurationBuilder => { configurationBuilder.AddJsonFile(configPath); });

            builder.ConfigureServices(services =>
            {

            });
        }
    }
}
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenChat.Persistence;
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
                // Remove the app's OpenChatDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<OpenChatDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add DB for acceptance tests
                services.AddDbContext<OpenChatDbContext>(options =>
                {
                    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OpenChatTestDB;Trusted_Connection=True;");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (OpenChatDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<OpenChatDbContext>();

                    // Ensure the database is created.
                    db.Database.EnsureDeleted();
                    db.Database.Migrate();
                }
            });
        }
    }
}
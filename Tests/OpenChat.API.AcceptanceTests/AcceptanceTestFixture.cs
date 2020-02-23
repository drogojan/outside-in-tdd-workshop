﻿using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
            builder.ConfigureServices(services =>
            {
                // // Remove the app's OpenChatDbContext registration.
                // var descriptor = services.SingleOrDefault(
                //     d => d.ServiceType ==
                //          typeof(DbContextOptions<OpenChatDbContext>));
                //
                // if (descriptor != null)
                // {
                //     services.Remove(descriptor);
                // }
                //
                // // Create a new service provider.
                // var sqlServerServiceProvider =
                //     new ServiceCollection()
                //         .AddEntityFrameworkSqlServer()
                //         .BuildServiceProvider();
                //
                // // Add DB for acceptance tests
                // services.AddDbContext<OpenChatDbContext>(options =>
                // {
                //     options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OpenChatDB_AcceptanceTests;Trusted_Connection=True;");
                //     options.UseInternalServiceProvider(sqlServerServiceProvider);
                // });
                //
                // // Build the service provider.
                // var sp = services.BuildServiceProvider();
                //
                // // Create a scope to obtain a reference to the database
                // // context (OpenChatDbContext).
                // using (var scope = sp.CreateScope())
                // {
                //     var scopedServices = scope.ServiceProvider;
                //     var dbContext = scopedServices.GetRequiredService<OpenChatDbContext>();
                //
                //     // Ensure the database is created and any migrations applied.
                //     dbContext.Database.Migrate();
                //     dbContext.WipeTables();
                // }
            });
        }
    }
}
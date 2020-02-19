using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenChat.Persistence;
using OpenChat.Test.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using TestSupport.Attributes;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Abstractions;

namespace OpenChat.Persistence.IntegrationTests
{
    public class IntegrationTests : IClassFixture<DbMigrationFixture>
    {
        //protected OpenChatDbContext DbContext { get; private set; }

        //private readonly ILoggerFactory _loggerFactory;
        // private readonly ITestOutputHelper testOutputHelper;

        DbContextOptions<OpenChatDbContext> dbContextOptions;
        // private ITestOutputHelper testOutput;

        public IntegrationTests(DbMigrationFixture dbMigrationFixture, ITestOutputHelper testOutputHelper)
        {
            //_loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    builder.ClearProviders();
            //    builder.AddConsole();
            //    builder.AddXUnit(testOutputHelper);
            //});

            // this.testOutputHelper = testOutputHelper;
            dbContextOptions = this.CreateUniqueClassOptionsWithLogging<OpenChatDbContext>(log => testOutputHelper.WriteLine(log.Message));
            var dbContext = new OpenChatDbContext(dbContextOptions);
            // dbContext.CreateEmptyViaWipe();
            dbMigrationFixture.Migrate(dbContext);
            // dbContext.Database.Migrate();
            dbContext.WipeTables();
        }

        protected OpenChatDbContext DbContext => GetDbContext();

        private OpenChatDbContext GetDbContext()
        {
            return new OpenChatDbContext(dbContextOptions);
            // if (dbContextInstance == null)
            // {
            //     var options = dbContextOptions;
            //     var dbContext = new OpenChatDbContext(options);
            //     dbContext.CreateEmptyViaWipe();

            //     dbContextInstance = dbContext;
            // }

            // return dbContextInstance;
        }

        // [RunnableInDebugOnly(Skip = "SkipedOnDebug")]
        // public void DeleteAllTestDatabasesOk()
        // {
        //     var numDeleted = DatabaseTidyHelper
        //         .DeleteAllUnitTestDatabases();
        //     testOutputHelper.WriteLine(
        //         "This deleted {0} databases.", numDeleted);
        // }
    }
}
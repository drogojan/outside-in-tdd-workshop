using Microsoft.Extensions.Logging;
using OpenChat.Persistence;
using System;
using TestSupport.Attributes;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Abstractions;

namespace OpenChat.Persistance.IntegrationTests
{
    public class IntegrationTests
    {
        //protected OpenChatDbContext DbContext { get; private set; }

        //private readonly ILoggerFactory _loggerFactory;
        private readonly ITestOutputHelper testOutputHelper;
        private OpenChatDbContext dbContextInstance;

        public IntegrationTests(ITestOutputHelper testOutputHelper)
        {
            //_loggerFactory = LoggerFactory.Create(builder =>
            //{
            //    builder.ClearProviders();
            //    builder.AddConsole();
            //    builder.AddXUnit(testOutputHelper);
            //});
            
            this.testOutputHelper = testOutputHelper;
        }

        protected OpenChatDbContext DbContext => GetDbContext();

        private OpenChatDbContext GetDbContext()
        {
            if (dbContextInstance == null)
            {
                var options = this.CreateUniqueClassOptionsWithLogging<OpenChatDbContext>(log => testOutputHelper.WriteLine(log.Message));
                var dbContext = new OpenChatDbContext(options);
                dbContext.CreateEmptyViaWipe();

                dbContextInstance = dbContext; 
            }

            return dbContextInstance;
        }

        [RunnableInDebugOnly(Skip="SkipedOnDebug")]
        public void DeleteAllTestDatabasesOk()
        {
            var numDeleted = DatabaseTidyHelper
                .DeleteAllUnitTestDatabases();
            testOutputHelper.WriteLine(
                "This deleted {0} databases.", numDeleted);
        }
    }
}
using TestSupport.Attributes;
using TestSupport.EfHelpers;
using Xunit;
using Xunit.Abstractions;

namespace OpenChat.Persistance.IntegrationTests.UnitCommands
{
    public class DatabaseCommands
    {
        private ITestOutputHelper testOutputHelper;

        public DatabaseCommands(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        // [RunnableInDebugOnly()]
        // [Trait("TraitKey", "TraitValue")]
        [Fact(Skip="Deletes DBs")]
        public void DeleteAllTestDatabasesOk()
        {
            var numDeleted = DatabaseTidyHelper
                .DeleteAllUnitTestDatabases();
            testOutputHelper.WriteLine(
                "This deleted {0} databases.", numDeleted);
        }
    }
}
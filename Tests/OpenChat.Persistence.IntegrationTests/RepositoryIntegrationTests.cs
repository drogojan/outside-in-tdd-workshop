using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using TestSupport.EfHelpers;

namespace OpenChat.Persistence.IntegrationTests
{
    public class RepositoryIntegrationTests : IClassFixture<DbMigrationFixture>
    {
        // DbContextOptions<OpenChatDbContext> dbContextOptions;
        //
        // public RepositoryIntegrationTests(DbMigrationFixture dbMigrationFixture, ITestOutputHelper testOutputHelper)
        // {
        //     dbContextOptions = this.CreateUniqueClassOptionsWithLogging<OpenChatDbContext>(log => testOutputHelper.WriteLine(log.Message));
        //     var dbContext = new OpenChatDbContext(dbContextOptions);
        //     dbContext.Database.Migrate();
        //     dbContext.WipeTables();
        // }

        // public OpenChatDbContext DbContext => new OpenChatDbContext(dbContextOptions);
    }
}
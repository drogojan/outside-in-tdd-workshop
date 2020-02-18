using Microsoft.EntityFrameworkCore;
using OpenChat.Persistence;
using OpenChat.Test.Infrastructure.Extensions;
using Xunit.Abstractions;

namespace OpenChat.Persistance.IntegrationTests
{
    public class DbMigrationFixture
    {
        private bool wasMigrated;
        public void Migrate(OpenChatDbContext dbContext)
        {
            if(!wasMigrated)
            {
                dbContext.Database.Migrate();
                wasMigrated = true;
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace OpenChat.Persistence.IntegrationTests
{
    public class DbMigrationFixture
    {
        private bool wasMigrated;
        public void Migrate(DbContext dbContext)
        {
            if (wasMigrated)
                return;

            dbContext.Database.Migrate();
            wasMigrated = true;
        }
    }
}
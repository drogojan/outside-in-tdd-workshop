using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpenChat.Test.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static void WipeTables(this DbContext dbContext)
        {
            IEnumerable<string> tablesToDeleteInOrder = new List<string> { "Users" };

            var wipeTablesSqlCommand = string.Join(";", tablesToDeleteInOrder.Select(tableName => $"DELETE FROM {tableName}"));

            dbContext.Database.ExecuteSqlRaw(wipeTablesSqlCommand);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenChat.Test.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static void WipeTables(this DbContext dbContext)
        {
            IEnumerable<string> tablesToDeleteInOrder = new List<string> {  };

            var wipeTablesSqlCommand = string.Join(";", tablesToDeleteInOrder.Select(tableName => $"DELETE FROM {tableName}"));

            dbContext.Database.ExecuteSqlRaw(wipeTablesSqlCommand);
        }
    }
}

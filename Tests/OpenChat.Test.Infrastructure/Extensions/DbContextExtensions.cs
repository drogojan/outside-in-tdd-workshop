using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OpenChat.Persistence;

namespace OpenChat.Test.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static void WipeTables(this OpenChatDbContext dbContext)
        {
            IEnumerable<string> tablesToDeleteInOrder = new List<string> { "Followings", "Posts", "Users" };

            var wipeTablesSqlCommand = string.Join(";", tablesToDeleteInOrder.Select(tableName => $"DELETE FROM {tableName}"));

            dbContext.Database.ExecuteSqlRaw(wipeTablesSqlCommand);
        }
    }
}
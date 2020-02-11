using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenChat.Persistence;
using TestSupport.EfHelpers;
using Xunit.Abstractions;

namespace OpenChat.Test.Infrastructure.Factories
{
    public class OpenChatDbContextFactory
    {
        public static OpenChatDbContext GetDbContext(ProviderType providerType, ILoggerFactory loggerFactory)
        {
            DbContextOptionsBuilder<OpenChatDbContext> dbContextOptionsBuilder =
                new DbContextOptionsBuilder<OpenChatDbContext>();

            dbContextOptionsBuilder.UseLoggerFactory(loggerFactory);

            switch (providerType)
            {
                case ProviderType.InMemory:
                    dbContextOptionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
                    break;
                case ProviderType.SqlServer:
                    dbContextOptionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OpenChatTestDB;Trusted_Connection=True;");
                    dbContextOptionsBuilder.EnableSensitiveDataLogging();
                    break;
            }

            var dbContext = new OpenChatDbContext(dbContextOptionsBuilder.Options);

            if (providerType == ProviderType.InMemory)
                return dbContext;

            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();

            return dbContext;
        }
    }
}
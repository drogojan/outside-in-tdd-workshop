using Microsoft.EntityFrameworkCore;
using OpenChat.Domain;

namespace OpenChat.Persistence
{
    public class OpenChatDbContext : DbContext
    {
        public OpenChatDbContext(DbContextOptions<OpenChatDbContext> dbContextOptions) 
            : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
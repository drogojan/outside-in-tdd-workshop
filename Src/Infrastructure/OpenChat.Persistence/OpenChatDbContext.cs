using Microsoft.EntityFrameworkCore;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistence
{
    public class OpenChatDbContext : DbContext
    {
        public OpenChatDbContext(DbContextOptions<OpenChatDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
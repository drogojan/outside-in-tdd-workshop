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

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OpenChatDbContext).Assembly);
        }
    }
}
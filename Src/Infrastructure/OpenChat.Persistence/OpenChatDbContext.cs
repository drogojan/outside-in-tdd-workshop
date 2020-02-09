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
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OpenChatDbContext).Assembly);
        }
    }
}
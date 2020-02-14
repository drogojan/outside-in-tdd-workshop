using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.About)
                .HasMaxLength(100);

            builder.HasAlternateKey(u => u.Username);
        }
    }
}
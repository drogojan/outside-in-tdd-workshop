using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.UserId)
                .IsRequired();
            builder.Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(p => p.DateTime)
                .IsRequired();
            builder.HasOne(p => p.User);
        }
    }
}
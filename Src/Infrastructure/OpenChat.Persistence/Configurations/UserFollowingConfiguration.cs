using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OpenChat.Persistence.Configurations
{
    public class UserFollowingConfigurations : IEntityTypeConfiguration<UserFollowing>
    {
        public void Configure(EntityTypeBuilder<UserFollowing> builder)
        {
            builder.HasKey(uf => new { uf.FollowerId, uf.FolloweeId });
            // TODO - many to many relashionship
        }
    }
}
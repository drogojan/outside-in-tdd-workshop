using System;
using OpenChat.Domain.Entities;

namespace OpenChat.Test.Infrastructure.Builders
{
    public class FollowingBuilder
    {
        private Guid _followerId = Guid.NewGuid();
        private Guid _followeeId = Guid.NewGuid();

        public static FollowingBuilder AFollowing()
        {
            return new FollowingBuilder();
        }

        public Following Build() =>
          new Following
          {
              FollowerId = _followerId,
              FolloweeId = _followeeId
          };

        public FollowingBuilder WithFollowerId(Guid value)
        {
            _followerId = value;
            return this;
        }

        public FollowingBuilder WithFolloweeId(Guid value)
        {
            _followeeId = value;
            return this;
        }
    }
}

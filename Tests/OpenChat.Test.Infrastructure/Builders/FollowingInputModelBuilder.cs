using System;
using OpenChat.Application.Followings;

namespace OpenChat.Test.Infrastructure.Builders
{
    public class FollowingInputModelBuilder
    {
        private Guid _followerId = Guid.NewGuid();
        private Guid _followeeId = Guid.NewGuid();

        public static FollowingInputModelBuilder AFollowingInputModel()
        {
            return new FollowingInputModelBuilder();
        }

        public FollowingInputModel Build() =>
          new FollowingInputModel
          {
              FollowerId = _followerId,
              FolloweeId = _followeeId
          };

        public FollowingInputModelBuilder WithFollowerId(Guid value)
        {
            _followerId = value;
            return this;
        }

        public FollowingInputModelBuilder WithFolloweeId(Guid value)
        {
            _followeeId = value;
            return this;
        }
    }
}

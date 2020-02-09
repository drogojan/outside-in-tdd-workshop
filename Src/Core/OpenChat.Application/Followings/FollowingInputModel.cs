using System;

namespace OpenChat.Application.Followings
{
    public class FollowingInputModel
    {
        public Guid FollowerId { get; set; }
        public Guid FolloweeId { get; set; }
    }
}
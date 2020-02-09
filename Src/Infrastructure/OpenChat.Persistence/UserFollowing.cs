using System;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistence
{
    public class UserFollowing
    {
        public Guid FollowerId { get; set; }
        public User FollowingUser  { get; set; }
        public Guid FolloweeId { get; set; }
        public User FollowedUser { get; set; }
    }
}
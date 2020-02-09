using System;

namespace OpenChat.Domain.Entities
{
    public class Following
    {
        public Guid FollowerId { get; set; }
        public Guid FolloweeId { get; set; }
    }
}
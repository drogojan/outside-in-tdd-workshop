using System;

namespace OpenChat.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public User User { get; set; }
    }
}
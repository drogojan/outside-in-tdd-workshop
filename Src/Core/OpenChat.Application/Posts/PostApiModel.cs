using System;

namespace OpenChat.Application.Posts
{
    public class PostApiModel
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
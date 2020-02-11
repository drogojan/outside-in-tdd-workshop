using System;
namespace OpenChat.API.AcceptanceTests.Models
{
    public class CreatedPost
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public string DateTime { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CreatedPost post &&
                   PostId.Equals(post.PostId) &&
                   UserId.Equals(post.UserId) &&
                   Text == post.Text &&
                   DateTime == post.DateTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PostId, UserId, Text, DateTime);
        }
    }
}
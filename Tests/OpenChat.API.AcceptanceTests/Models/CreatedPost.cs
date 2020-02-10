using System;
namespace OpenChat.API.AcceptanceTests.Models
{
    public class CreatedPost
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
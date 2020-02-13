using System;

namespace OpenChat.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
    }
}
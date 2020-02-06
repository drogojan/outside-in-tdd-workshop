using System;
using OpenChat.Common;

namespace OpenChat.Infrastructure
{
    public class UtcDateTime : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
using System;
using OpenChat.Common;

namespace OpenChat.Infrastructure
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid Next()
        {
            return new Guid();
        }
    }
}
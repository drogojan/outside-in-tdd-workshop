using System;

namespace OpenChat.Common
{
    public interface IGuidGenerator
    {
        Guid Next();
    }
}
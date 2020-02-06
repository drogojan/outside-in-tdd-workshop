using System;
namespace OpenChat.Common
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}
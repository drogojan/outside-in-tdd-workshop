using System;
using OpenChat.Common;

namespace OpenChat.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
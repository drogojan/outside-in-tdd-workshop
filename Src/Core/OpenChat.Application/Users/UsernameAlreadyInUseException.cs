using System;

namespace OpenChat.Application.Users
{
    public class UsernameAlreadyInUseException : Exception
    {
        public UsernameAlreadyInUseException() : base("Username already in use")
        {
            
        }
    }
}
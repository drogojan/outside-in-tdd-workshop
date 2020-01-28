using System;
namespace OpenChat.Application.Users
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid credentials")
        {
        }
    }
}
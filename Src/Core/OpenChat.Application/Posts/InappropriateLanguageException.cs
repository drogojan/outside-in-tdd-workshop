using System;
namespace OpenChat.Application.Posts
{
    public class InappropriateLanguageException : Exception
    {
        public InappropriateLanguageException() : base("Post contains inappropriate language")
        {
        }
    }
}
using System;

namespace OpenChat.Application.Followings
{
    public class FollowingAlreadyExistsException : Exception
    {
        public FollowingAlreadyExistsException()
            : base("Following already exists")
        {
        }
    }
}
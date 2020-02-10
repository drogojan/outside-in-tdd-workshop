using System;
using System.Collections.Generic;
using OpenChat.Application.Users;

namespace OpenChat.Application.Followings
{
    public interface IFollowingService
    {
        void CreateFollowing(FollowingInputModel following);
        IEnumerable<UserApiModel> UsersFollowedBy(Guid userId);
    }
}
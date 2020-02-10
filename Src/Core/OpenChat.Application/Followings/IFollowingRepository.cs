using System;
using System.Collections.Generic;
using OpenChat.Domain.Entities;

namespace OpenChat.Application.Followings
{
    public interface IFollowingRepository
    {
        void Add(Following following);
        bool IsFollowingRegistered(Following following);
        IEnumerable<User> UsersFollowedBy(Guid guid);
    }
}
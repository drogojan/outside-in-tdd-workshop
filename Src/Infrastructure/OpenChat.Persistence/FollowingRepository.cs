using System;
using System.Collections.Generic;
using System.Linq;
using OpenChat.Application.Followings;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistence
{
    public class FollowingRepository : IFollowingRepository
    {
        private OpenChatDbContext dbContext;

        public FollowingRepository(OpenChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Following following)
        {
            dbContext.Followings.Add(new Following
            {
                FollowerId = following.FollowerId,
                FolloweeId = following.FolloweeId
            });
            dbContext.SaveChanges();
        }

        public bool IsFollowingRegistered(Following following)
        {
            return dbContext.Followings.Any(uf =>
                uf.FollowerId == following.FollowerId
                && uf.FolloweeId == following.FolloweeId
            );
        }

        public IEnumerable<User> UsersFollowedBy(Guid userId)
        {
            var followeeIds = dbContext.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FolloweeId)
                .ToList();

            return dbContext.Users.Where(u => followeeIds.Contains(u.Id));
        }
    }
}
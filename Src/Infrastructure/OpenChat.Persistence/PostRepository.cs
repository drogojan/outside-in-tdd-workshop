using System;
using System.Collections.Generic;
using System.Linq;
using OpenChat.Application.Posts;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistence
{
    public class PostRepository : IPostRepository
    {
        private OpenChatDbContext dbContext;

        public PostRepository(OpenChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Post post)
        {
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();
        }

        public IEnumerable<Post> PostsBy(Guid userId)
        {
            return dbContext.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.DateTime)
                .ToList();
        }

        public IEnumerable<Post> WallPostsFor(Guid userId)
        {
            var userIds = new List<Guid> { userId };

            var foloweesId = dbContext.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FolloweeId);

            userIds.AddRange(foloweesId);

            return dbContext.Posts
                .Where(p => userIds.Contains(p.UserId))
                .OrderByDescending(p => p.DateTime);
        }
    }
}
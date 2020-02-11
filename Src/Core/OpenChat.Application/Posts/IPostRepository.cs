using System;
using System.Collections.Generic;
using OpenChat.Domain.Entities;

namespace OpenChat.Application.Posts
{
    public interface IPostRepository
    {
        void Add(Post post);
        IEnumerable<Post> PostsBy(Guid userId);
        IEnumerable<Post> WallPostsFor(Guid userId);
    }
}
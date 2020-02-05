using System;
using System.Collections.Generic;
using OpenChat.Application.Posts;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistence
{
    public class PostRepository : IPostRepository
    {
        public void Add(Post post)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Post> PostsBy(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
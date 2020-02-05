using System;
using System.Collections.Generic;

namespace OpenChat.Application.Posts
{
    public interface IPostService
    {
        PostApiModel CreatePost(Guid userId, string postText);
        IEnumerable<PostApiModel> PostsBy(Guid userId);
    }
}
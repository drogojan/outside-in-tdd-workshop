using System;

namespace OpenChat.Application.Posts
{
    public interface IPostService
    {
        PostApiModel CreatePost(Guid userId, string postText);
    }
}
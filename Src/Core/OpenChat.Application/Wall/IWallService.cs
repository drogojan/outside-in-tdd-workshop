using System;
using System.Collections.Generic;
using OpenChat.Application.Posts;

namespace OpenChat.Application.Wall
{
    public interface IWallService
    {
         IEnumerable<PostApiModel> GetWallPosts(Guid userId);
    }
}
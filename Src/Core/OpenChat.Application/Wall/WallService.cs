using System;
using System.Collections.Generic;
using System.Linq;
using OpenChat.Application.Posts;

namespace OpenChat.Application.Wall
{
    public class WallService : IWallService
    {
        private IPostRepository postRepository;

        public WallService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public IEnumerable<PostApiModel> GetWallPosts(Guid userId)
        {
            return postRepository.WallPostsFor(userId)
                    .Select(p => new PostApiModel {
                        PostId = p.Id,
                        UserId = p.UserId,
                        Text = p.Text,
                        DateTime = p.DateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'")
                    });
        }
    }
}
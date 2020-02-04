using System;
using OpenChat.Common;
using OpenChat.Domain.Entities;

namespace OpenChat.Application.Posts
{
    public class PostService : IPostService
    {
        private readonly ILanguageService languageService;
        private readonly IGuidGenerator guidGenerator;
        private readonly IDateTime dateTime;
        private readonly IPostRepository postRepository;


        public PostService(ILanguageService languageService, IGuidGenerator guidGenerator, IDateTime dateTime, IPostRepository postRepository)
        {
            this.languageService = languageService;
            this.guidGenerator = guidGenerator;
            this.dateTime = dateTime;
            this.postRepository = postRepository;
        }

        public PostApiModel CreatePost(Guid userId, string postText)
        {
            if(languageService.IsInappropriate(postText)) {
                throw new InappropriateLanguageException();
            }

            Post post = new Post {
                Id = guidGenerator.Next(),
                UserId = userId,
                Text = postText,
                DateTime = dateTime.Now
            };

            postRepository.Add(post);

            return new PostApiModel {
                PostId = post.Id,
                UserId = post.UserId,
                Text = post.Text,
                DateTime = post.DateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'")
            };
        }
    }
}
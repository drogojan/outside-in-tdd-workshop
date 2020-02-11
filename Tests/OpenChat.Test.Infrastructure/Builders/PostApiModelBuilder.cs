using System;
using OpenChat.Application.Posts;

namespace OpenChat.Test.Infrastructure
{
    public class PostApiModelBuilder
    {
        private Guid _userId = Guid.NewGuid();
        private Guid _postId = Guid.NewGuid();
        private string _text = "";
        private string _dateTime = "";

        public static PostApiModelBuilder APostApiModel()
        {
            return new PostApiModelBuilder();
        }

        public PostApiModel Build() =>
          new PostApiModel
          {
              UserId = _userId,
              PostId = _postId,
              Text = _text,
              DateTime = _dateTime
          };

        public PostApiModelBuilder WithUserId(Guid value)
        {
            _userId = value;
            return this;
        }

        public PostApiModelBuilder WithPostId(Guid value)
        {
            _postId = value;
            return this;
        }

        public PostApiModelBuilder WithText(string value)
        {
            _text = value;
            return this;
        }

        public PostApiModelBuilder WithDateTime(string value)
        {
            _dateTime = value;
            return this;
        }
    }
}

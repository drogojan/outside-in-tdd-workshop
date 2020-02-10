using System;
using OpenChat.API.AcceptanceTests.Models;

namespace OpenChat.API.AcceptanceTests.Builders
{
    public class PostBuilder
    {
        private string _text = "text";

        public static PostBuilder APost()
        {
          return new PostBuilder();
        }
        public Post Build() =>
          new Post
          {
              Text = _text
          };

        public PostBuilder WithText(string value)
        {
            _text = value;
            return this;
        }
    }
}

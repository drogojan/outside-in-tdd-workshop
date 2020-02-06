using System;
using OpenChat.Domain.Entities;

namespace OpenChat.Test.Infrastructure.Builders
{
  public class PostBuilder
  {
    private Guid _id = Guid.NewGuid();
    private Guid _userId = Guid.NewGuid();
    private string _text = "text";
    private DateTime _dateTime = new DateTime(1970, 1, 1);

    public static PostBuilder APost()
    {
      return new PostBuilder();
    }

    public Post Build() =>
      new Post
      {
        Id = _id,
        UserId = _userId,
        Text = _text,
        DateTime = _dateTime
      };

    public PostBuilder WithId(Guid value)
    {
      _id = value;
      return this;
    }

    public PostBuilder WithUserId(Guid value)
    {
      _userId = value;
      return this;
    }

    public PostBuilder WithText(string value)
    {
      _text = value;
      return this;
    }

    public PostBuilder WithDateTime(DateTime value)
    {
      _dateTime = value;
      return this;
    }
  }
}

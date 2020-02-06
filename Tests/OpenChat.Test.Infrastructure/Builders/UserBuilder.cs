using System;
using OpenChat.Domain.Entities;

namespace OpenChat.Test.Infrastructure.Builders
{
  public class UserBuilder
  {
    private Guid _id = Guid.NewGuid();
    private string _username = "username";
    private string _password = "password";
    private string _about = "about";

    public static UserBuilder AUser()
    {
      return new UserBuilder();
    }

    public User Build() =>
      new User
      {
        Id = _id,
        Username = _username,
        Password = _password,
        About = _about
      };

    public UserBuilder WithId(Guid value)
    {
      _id = value;
      return this;
    }

    public UserBuilder WithUsername(string value)
    {
      _username = value;
      return this;
    }

    public UserBuilder WithPassword(string value)
    {
      _password = value;
      return this;
    }

    public UserBuilder WithAbout(string value)
    {
      _about = value;
      return this;
    }
  }
}

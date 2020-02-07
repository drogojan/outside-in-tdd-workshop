using System;
using OpenChat.API.AcceptanceTests.Models;

namespace OpenChat.API.AcceptanceTests.Builders
{
    public class UserBuilder
    {
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
              Username = _username,
              Password = _password,
              About = _about
          };

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

using System;
using OpenChat.Application.Users;

namespace OpenChat.Test.Infrastructure.Builders
{
    public class RegistrationInputModelBuilder
    {
        private string _username = "username";
        private string _password = "password";
        private string _about = "about";

        public static RegistrationInputModelBuilder AUserRegistration()
        {
            return new RegistrationInputModelBuilder();
        }

        public RegistrationInputModel Build() =>
          new RegistrationInputModel
          {
              Username = _username,
              Password = _password,
              About = _about
          };

        public RegistrationInputModelBuilder WithUsername(string value)
        {
            _username = value;
            return this;
        }

        public RegistrationInputModelBuilder WithPassword(string value)
        {
            _password = value;
            return this;
        }

        public RegistrationInputModelBuilder WithAbout(string value)
        {
            _about = value;
            return this;
        }
    }
}

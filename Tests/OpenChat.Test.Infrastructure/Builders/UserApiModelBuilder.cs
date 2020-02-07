using System;
using OpenChat.Application.Users;

namespace OpenChat.Test.Infrastructure.Builders
{
    public class UserApiModelBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _username = "username";
        private string _about = "about";

        public static UserApiModelBuilder AUserApiModel()
        {
            return new UserApiModelBuilder();
        }

        public UserApiModel Build() =>
          new UserApiModel
          {
              Id = _id,
              Username = _username,
              About = _about
          };

        public UserApiModelBuilder WithId(Guid value)
        {
            _id = value;
            return this;
        }

        public UserApiModelBuilder WithUsername(string value)
        {
            _username = value;
            return this;
        }

        public UserApiModelBuilder WithAbout(string value)
        {
            _about = value;
            return this;
        }
    }
}

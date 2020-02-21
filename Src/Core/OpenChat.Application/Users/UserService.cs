using OpenChat.Common;
using OpenChat.Domain;

namespace OpenChat.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IGuidGenerator guidGenerator;
        private readonly IUserRepository userRepository;

        public UserService(IGuidGenerator guidGenerator, IUserRepository userRepository)
        {
            this.guidGenerator = guidGenerator;
            this.userRepository = userRepository;
        }

        public UserVm CreateUser(UserRegistration userRegistration)
        {
            if(userRepository.IsUsernameTaken(userRegistration.Username))
                throw new UsernameAlreadyInUseException();

            var user = new User
            {
                Id = guidGenerator.Next(),
                Username = userRegistration.Username,
                Password = userRegistration.Password,
                About = userRegistration.About
            };

            userRepository.Add(user);

            return new UserVm
            {
                Id = user.Id,
                Username = user.Username,
                About = user.About
            };
        }
    }
}
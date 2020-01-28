using OpenChat.Application.Users;
using OpenChat.Common;
using OpenChat.Domain.Entities;

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
        public RegisteredUserApiModel CreateUser(RegistrationInputModel registrationData)
        {
            if(userRepository.IsUsernameTaken(registrationData.Username))
                throw new UsernameAlreadyInUseException();

            User user = new User
            {
                Id = guidGenerator.Next(),
                Username = registrationData.Username,
                Password = registrationData.Password,
                About = registrationData.About
            };

            this.userRepository.Add(user);

            return new RegisteredUserApiModel
            {
                Id = user.Id,
                Username = user.Username,
                About = user.About
            };
        }
    }
}
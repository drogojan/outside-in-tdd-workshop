using OpenChat.Domain.Entities;

namespace OpenChat.Application.Users
{
    public class LoginService : ILoginService
    {
        private IUserRepository userRepository;

        public LoginService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserApiModel Login(UserCredentials userCredentials)
        {
            User user = userRepository.UserFor(userCredentials) ?? throw new InvalidCredentialsException();
            return new UserApiModel {
                Id = user.Id,
                Username = user.Username,
                About = user.About
            };
        }
    }
}
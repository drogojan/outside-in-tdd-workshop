namespace OpenChat.Application.Users
{
    public class LoginService : ILoginService
    {
        private IUserRepository userRepository;

        public LoginService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public LoggedInUser Login(UserCredentials userCredentials)
        {
            return userRepository.UserFor(userCredentials);
        }
    }
}
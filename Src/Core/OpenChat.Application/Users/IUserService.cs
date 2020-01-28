namespace OpenChat.Application.Users
{
    public interface IUserService
    {
        RegisteredUserApiModel CreateUser(RegistrationInputModel registrationData);
        LoggedInUser Login(UserCredentials userCredentials);
    }
}
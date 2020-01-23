namespace OpenChat.Application.Users
{
    public interface IUserService
    {
        UserApiModel CreateUser(UserInputModel registrationData);
    }
}
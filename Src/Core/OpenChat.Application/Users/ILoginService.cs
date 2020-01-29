namespace OpenChat.Application.Users
{
    public interface ILoginService
    {
        LoggedInUser Login(UserCredentials userCredentials);
    }
}
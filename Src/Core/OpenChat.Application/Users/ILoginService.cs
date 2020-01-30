namespace OpenChat.Application.Users
{
    public interface ILoginService
    {
        UserApiModel Login(UserCredentials userCredentials);
    }
}
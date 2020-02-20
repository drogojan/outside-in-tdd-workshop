namespace OpenChat.Application.Users
{
    public interface IUserService
    {
        UserVm CreateUser(UserRegistration userRegistration);
    }
}
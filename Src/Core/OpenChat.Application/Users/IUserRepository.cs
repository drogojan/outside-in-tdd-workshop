using OpenChat.Domain;

namespace OpenChat.Application.Users
{
    public interface IUserRepository
    {
        void Add(User user);
        bool IsUsernameTaken(string username);
    }
}
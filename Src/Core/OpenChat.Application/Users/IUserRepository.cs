using OpenChat.Domain.Entities;

namespace OpenChat.Application.Users
{
    public interface IUserRepository
    {
        void Add(User user);
    }
}
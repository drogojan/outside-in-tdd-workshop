using System.Collections.Generic;
using OpenChat.Domain.Entities;

namespace OpenChat.Application.Users
{
    public interface IUserRepository
    {
        void Add(User user);
        bool IsUsernameTaken(string username);
        User UserFor(UserCredentials userCredentials);
        IEnumerable<User> AllUsers();
    }
}
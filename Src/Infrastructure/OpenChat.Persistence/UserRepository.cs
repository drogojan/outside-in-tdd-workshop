using OpenChat.Application.Users;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistance
{
    public class UserRepository : IUserRepository
    {
        public void Add(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool IsUsernameTaken(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}
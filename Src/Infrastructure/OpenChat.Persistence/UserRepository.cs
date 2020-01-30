using System.Linq;
using OpenChat.Application.Users;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistence
{
    public class UserRepository : IUserRepository
    {
        private OpenChatDbContext dbContext;

        public UserRepository(OpenChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public bool IsUsernameTaken(string username)
        {
            return dbContext.Users.Any(u => u.Username == username);
        }

        public User UserFor(UserCredentials userCredentials)
        {
            throw new System.NotImplementedException();
        }
    }
}
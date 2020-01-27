using System.Linq;
using OpenChat.Application.Users;
using OpenChat.Domain.Entities;
using OpenChat.Persistence;

namespace OpenChat.Persistance
{
    public class UserRepository : IUserRepository
    {
        private OpenChatDbContext dbContext;

        public UserRepository(OpenChatDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbContext.Database.EnsureCreated();
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
    }
}
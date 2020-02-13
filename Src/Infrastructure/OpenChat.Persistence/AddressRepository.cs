using System;
using System.Collections.Generic;
using System.Linq;
using OpenChat.Domain.Entities;

namespace OpenChat.Persistence
{
    public class AddressRepository
    {
        private readonly OpenChatDbContext dbContext;

        public AddressRepository(OpenChatDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Address address)
        {
            dbContext.Add(address);
            dbContext.SaveChanges();
        }

        public IEnumerable<Address> AddressesBy(Guid userId)
        {
            return dbContext.Addresses
                .Where(a => a.UserId == userId)
                .ToList();
        }


    }
}
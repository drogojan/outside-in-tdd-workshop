using System;
using System.Collections.Generic;
using FluentAssertions;
using OpenChat.Domain.Entities;
using OpenChat.Persistence;
using OpenChat.Test.Infrastructure.Builders;
using Xunit;
using Xunit.Abstractions;

namespace OpenChat.Persistance.IntegrationTests
{
    public class AddressRespositoryTests : IntegrationTests
    {
        public AddressRespositoryTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public void Return_addresses_of_a_user()
        {
            var JACK = UserBuilder.AUser().WithUsername("JACK").Build();
            var userRepository = new UserRepository(DbContext);
            userRepository.Add(JACK);

            var address1 = new Address { Id = Guid.NewGuid(), UserId = JACK.Id, Line1 = "1_1", Line2 = "1_2" };
            var address2 = new Address { Id = Guid.NewGuid(), UserId = JACK.Id, Line1 = "2_1", Line2 = "2_2" };

            var sut = new AddressRepository(DbContext);
            
            sut.Add(address1);
            sut.Add(address2);

            IEnumerable<Address> addresses = sut.AddressesBy(JACK.Id);

            addresses.Should().BeEquivalentTo(address1, address2);
        }
    }
}
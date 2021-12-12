using System;
using Management.DomainModels;
using Management.Enum;
using Management.Mapping;
using Xunit;
using Xunit.Abstractions;

using DomainUser = Management.DomainModels.User;
using StorageUser = Common.StorageModels.User;

namespace Test.Management.Unit
{
    public class DomainToStorageMapperTests
    {
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainToStorageMapperTests"/> class.
        /// </summary>
        /// <param name="output">test output helper.</param>
        public DomainToStorageMapperTests(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// Test to see if mapping from DomainUser to StorageUser works as expected.
        /// </summary>
        [Fact]
        public void UserToStorageTest()
        {
            var domainUser = new DomainUser(UserId.Wrap("0"), FullName.Wrap("domain user"), PassportId.Wrap("CA-12345"), DateTime.UtcNow, CountryCode.CA);

            var storageUser = DomainToStorageMapper.ToStorage(domainUser);

            Assert.Equal(storageUser.UserId, domainUser.UserId.Value);
            Assert.Equal(storageUser.FullName, domainUser.FullName.Value);
            Assert.Equal(storageUser.PassportId, domainUser.PassportId.Value);
            Assert.Equal(storageUser.CreatedAt.ToString(), domainUser.CreatedAt.ToString());
            Assert.Equal(storageUser.CountryCode, domainUser.CountryCode.ToString());
        }
    }
}

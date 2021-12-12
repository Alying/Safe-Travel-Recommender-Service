using System;
using Management.DomainModels;
using Management.Enum;
using Management.Mapping;
using Xunit;
using Xunit.Abstractions;

using ApiUser = Management.ApiModels.User;
using DomainUser = Management.DomainModels.User;

namespace Test.Management.Unit
{
    public class DomainToApiMapperTests
    {
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainToApiMapperTests"/> class.
        /// </summary>
        /// <param name="output">test output helper.</param>
        public DomainToApiMapperTests(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// Test to see if mapping from ApiUser to DomainUser works as expected.
        /// </summary>
        [Fact]
        public void UserToApiTest()
        {
            var domainUser = new DomainUser(UserId.Wrap("domainUserId"), FullName.Wrap("domainUserFullName"), PassportId.Wrap("domainUserPassportId"), DateTime.UtcNow, CountryCode.US);

            var apiUser = DomainToApiMapper.ToApi(domainUser);

            Assert.Equal(apiUser.UserId, domainUser.UserId.Value);
            Assert.Equal(apiUser.FullName, domainUser.FullName.Value);
            Assert.Equal(apiUser.PassportId, domainUser.PassportId.Value);
            Assert.Equal(apiUser.CreatedAt.ToString(), domainUser.CreatedAt.ToString());
            Assert.Equal(apiUser.CountryCode, domainUser.CountryCode.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Enum;
using Management.Repository;
using Moq;
using Storage.Interface;
using Xunit;
using Xunit.Abstractions;

using Management.DomainModels;
using Management.Enum;
using ApiComment = Management.ApiModels.Comment;
using ApiUser = Management.ApiModels.User;
using DomainUser = Management.DomainModels.User;

using Management.Mapping;

namespace Test.Management.Unit
{
    public class ApiToDomainMapperTests
    {
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiToDomainMapperTests"/> class.
        /// </summary>
        /// <param name="output">test output helper.</param>
        public ApiToDomainMapperTests(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// Test to see if mapping from ApiUser to DomainUser works as expected.
        /// </summary>
        [Fact]
        public void UserToDomainTest()
        {
            var apiUser = new ApiUser()
            {
                UserId = "apiUserId",
                FullName = "apiUserFullName",
                PassportId = "apiUserPassportId",
                CreatedAt = DateTime.Now,
                CountryCode = "CA",
            };

            var domainUser = ApiToDomainMapper.ToDomain(apiUser);

            Assert.Equal(apiUser.UserId, domainUser.UserId.Value);
            Assert.Equal(apiUser.FullName, domainUser.FullName.Value);
            Assert.Equal(apiUser.PassportId, domainUser.PassportId.Value);
            Assert.Equal(apiUser.CreatedAt.ToString(), domainUser.CreatedAt.ToString());
            Assert.Equal(apiUser.CountryCode, domainUser.CountryCode.ToString());
        }

        /// <summary>
        /// Test to see if mapping from ApiComment to DomainComment works as expected.
        /// </summary>
        [Fact]
        public void CommentToDomainTest()
        {
            var apiComment = new ApiComment()
            {
                UserIdStr = "apiUserId",
                CommentStr = "some random comment",
            };

            var domainComment = ApiToDomainMapper.ToDomain(new Location(CountryCode.US, State.Wrap("NY")), apiComment);

            Assert.Equal(apiComment.UserIdStr, domainComment.UserId.Value);
            Assert.Equal(apiComment.CommentStr, domainComment.CommentStr);
            Assert.Equal("US", domainComment.Location.CountryCode.ToString());
            Assert.Equal("NY", domainComment.Location.State.Value);
        }
    }
}

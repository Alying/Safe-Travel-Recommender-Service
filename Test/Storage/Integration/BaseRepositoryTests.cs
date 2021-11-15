using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.StorageModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Storage;
using Xunit;

namespace Test.Storage.Integration
{
    public class BaseRepositoryTests
    {
        private readonly string _tableName = "user";

        [Fact]
        public async Task GetAllAsync_ValidInput_Success()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var iConfigMock = mockRepo.Create<IConfiguration>();
            iConfigMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());

            var baseRepo = new BaseRepository(iConfigMock.Object);
            var result = await baseRepo.GetAllAsync<User>(_tableName);

            Assert.NotEmpty(result);
            mockRepo.VerifyAll();
        }

        [Fact]
        public async Task GetAsync_ValidInput_Success()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var iConfigMock = mockRepo.Create<IConfiguration>();
            iConfigMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());

            var baseRepo = new BaseRepository(iConfigMock.Object);
            var result = await baseRepo.GetAsync<User>(_tableName, "UserId", "1");

            Assert.Equal("Muzhi", result.FullName);
            Assert.Equal("1", result.UserId);
            Assert.Equal("Admin", result.CountryCode);
            mockRepo.VerifyAll();
        }

        [Fact]
        public async Task InsertAsync_ValidInput_Success()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var iConfigMock = mockRepo.Create<IConfiguration>();
            iConfigMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());

            var baseRepo = new BaseRepository(iConfigMock.Object);

            await baseRepo.InsertAsync<User>(_tableName, new List<string>[]
            {
                new List<string>{ "TestInsert1", "2", "Admin",  DateTime.UtcNow.ToString(), "123" },
            });

            var insertedData = await baseRepo.GetAsync<User>(_tableName, "UserId", "2");
            Assert.NotNull(insertedData);

            await baseRepo.DeleteAsync<User>(_tableName, "UserId", "2");

            mockRepo.VerifyAll();
        }

        [Fact]
        public async Task UpdateAsync_ValidInput_Success()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var iConfigMock = mockRepo.Create<IConfiguration>();
            iConfigMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());

            var baseRepo = new BaseRepository(iConfigMock.Object);

            await baseRepo.UpdateAsync(_tableName, "UserId", "1", new Dictionary<string, string>
            {
                { "UserRole", "Test" },
            });

            var updatedResult = await baseRepo.GetAsync<User>(_tableName, "UserId", "1");

            Assert.Equal("Muzhi", updatedResult.FullName);
            Assert.Equal("1", updatedResult.UserId);
            Assert.Equal("Test", updatedResult.CountryCode);

            await baseRepo.UpdateAsync(_tableName, "UserId", "1", new Dictionary<string, string>
            {
                { "UserRole", "Admin" },
            });
            mockRepo.VerifyAll();
        }
    }

    public class Section : IConfigurationSection
    {
        public string this[string key] { get => "Server=127.0.0.1; database=demo; UID=root; password=asesharp"; set => throw new NotImplementedException(); }

        public string Key => "ConnectionStrings";

        public string Path => throw new NotImplementedException();

        public string Value { get => "server=127.0.0.1; database=demo; uid=root; pwd=asesharp"; set => throw new NotImplementedException(); }
        // Data123!#

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.StorageModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Storage;
using Xunit;
using Xunit.Abstractions;

namespace Test.Storage.Integration
{
    public class BaseRepositoryTests : IDisposable
    {
        private readonly string _tableName = "user";
        private readonly ITestOutputHelper _output;
        private readonly MockRepository _mockRepo;
        private readonly BaseRepository _repository;

        public BaseRepositoryTests(ITestOutputHelper output)
        {
            this._output = output;
            _mockRepo = new MockRepository(MockBehavior.Strict);
            var iConfigMock = _mockRepo.Create<IConfiguration>();
            iConfigMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());
            _repository = new BaseRepository(iConfigMock.Object);
            _repository.DeleteAllAsync(_tableName).Wait();
            _repository.InsertAsync<User>(_tableName, new List<string>[]
            {
                new List<string>{ "TestInsert1", "2", "Admin",  DateTime.UtcNow.ToString(), "123" },
            }).Wait();
        }

        public void Dispose()
        {
            _repository.DeleteAllAsync(_tableName).Wait();
        }

        [Fact]
        public async Task GetAllAsync_ValidInput_Success()
        {
            var result = await _repository.GetAllAsync<User>(_tableName);
            Assert.NotEmpty(result);
            _mockRepo.VerifyAll();
        }

        [Fact]
        public async Task GetAsync_ValidInput_Success()
        {
            var result = await _repository.GetAsync<User>(_tableName, "UserId", "2");

            Assert.Equal("TestInsert1", result.UserName);
            Assert.Equal("2", result.UserId);
            Assert.Equal("Admin", result.UserRole);
            Assert.Equal("123", result.CountryCode);

            _mockRepo.VerifyAll();
        }

        [Fact]
        public async Task InsertAsync_ValidInput_Success()
        {
            await _repository.InsertAsync<User>(_tableName, new List<string>[]
            {
                new List<string>{ "TestInsert2", "3", "Admin",  DateTime.UtcNow.ToString(), "456" },
            });

            var insertedData = await _repository.GetAsync<User>(_tableName, "UserId", "3");
            Assert.NotNull(insertedData);

            _mockRepo.VerifyAll();
        }

        [Fact]
        public async Task UpdateAsync_ValidInput_Success()
        {
            await _repository.UpdateAsync(_tableName, "UserId", "2", new Dictionary<string, string>
            {
                { "UserRole", "Test" },
            });

            var updatedResult = await _repository.GetAsync<User>(_tableName, "UserId", "2");

            Assert.Equal("TestInsert1", updatedResult.UserName);
            Assert.Equal("2", updatedResult.UserId);
            Assert.Equal("Test", updatedResult.UserRole);

            _mockRepo.VerifyAll();
        }
    }

    public class Section : IConfigurationSection
    {
        public string this[string key] { get => "Server=localhost; port=3306; database=asesharptestdb; UID=root; password=asesharp"; set => throw new NotImplementedException(); }

        public string Key => "ConnectionStrings";

        public string Path => throw new NotImplementedException();

        public string Value { get => "Server=localhost; port=3306; database=asesharptestdb; UID=root; password=asesharp"; set => throw new NotImplementedException(); }

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

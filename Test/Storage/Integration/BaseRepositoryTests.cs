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
    /// <summary>
    /// Integration test for base repository
    /// </summary>
    public class BaseRepositoryTests : IDisposable
    {
        private const string TestFlag = "disable for CI/CD";

        private readonly string _tableName = "user";
        private readonly ITestOutputHelper _output;
        private readonly MockRepository _mockRepo;
        private readonly BaseRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepositoryTests"/> class.
        /// </summary>
        /// <param name="output">test output helper.</param>
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
                new List<string> { "TestInsert1", "2", "Admin",  DateTime.UtcNow.ToString(), "123" },
            }).Wait();
        }

        /// <summary>
        /// Remove all items from the table.
        /// </summary>
        public void Dispose()
        {
            _repository.DeleteAllAsync(_tableName).Wait();
        }

        /// <summary>
        /// Test to see if successfully get all table items
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [Fact(Skip = TestFlag)]
        public async Task GetAllAsync_ValidInput_Success()
        {
            var result = await _repository.GetAllAsync<User>(_tableName);
            Assert.NotEmpty(result);
            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to see if successfully get specific table item
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [Fact(Skip = TestFlag)]
        public async Task GetAsync_ValidInput_Success()
        {
            var result = await _repository.GetAsync<User>(_tableName, "UserId", "2");

            Assert.Equal("TestInsert1", result.UserName);
            Assert.Equal("2", result.UserId);
            Assert.Equal("Admin", result.UserRole);
            Assert.Equal("123", result.CountryCode);

            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to see if successfully insert new table item
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [Fact(Skip = TestFlag)]
        public async Task InsertAsync_ValidInput_Success()
        {
            await _repository.InsertAsync<User>(_tableName, new List<string>[]
            {
                new List<string> { "TestInsert2", "3", "Admin",  DateTime.UtcNow.ToString(), "456" },
            });

            var insertedData = await _repository.GetAsync<User>(_tableName, "UserId", "3");
            Assert.NotNull(insertedData);

            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to see if successfully update table item
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [Fact(Skip = TestFlag)]
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

    /// <summary>
    /// Representation of configuration section
    /// </summary>
    public class Section : IConfigurationSection
    {
        /// <summary>
        /// Gets database server config
        /// </summary>
        /// <param name="key">database server string.</param>
        public string this[string key] { get => "Server=localhost; port=3306; database=asesharptestdb; UID=root; password=asesharp"; set => throw new NotImplementedException(); }

        /// <summary>
        /// Gets connection string 
        /// </summary>
        public string Key => "ConnectionStrings";

        /// <summary>
        /// Gets connection path
        /// </summary>
        public string Path => throw new NotImplementedException();

        /// <summary>
        /// Gets or sets api key
        /// </summary>
        public string Value { get => "Server=localhost; port=3306; database=asesharptestdb; UID=root; password=asesharp"; set => throw new NotImplementedException(); }

        /// <summary>
        /// Gets the immediate descendant configuration sub-sections.
        /// </summary>
        /// <returns>configuration section.</returns>
        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a IChangeToken that can be used to observe when this configuration is reloaded.
        /// </summary>
        /// <returns>IChangeToken.</returns>
        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the specified ConfigurationSection object.
        /// </summary>
        /// <param name="key">key string.</param>
        /// <returns>IConfigurationSection.</returns>
        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}
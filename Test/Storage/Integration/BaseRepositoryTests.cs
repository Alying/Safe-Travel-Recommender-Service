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

using Common.StorageModels;

namespace Test.Storage.Integration
{
    public sealed class IgnoreOnCiCdFact : FactAttribute
    {
        // https://josephwoodward.co.uk/2019/01/skipping-xunit-tests-based-on-runtime-conditions
        public IgnoreOnCiCdFact()
        {
            if (IsCiCd())
            {
                Skip = "Ignore for CI/CD due to LOCAL_TEST env var not set.";
            }
        }

        private static bool IsCiCd()
        {
            return Environment.GetEnvironmentVariable("LOCAL_TEST") == null; 
        }
    }

    /// <summary>
    /// Integration test for base repository
    /// </summary>
    public class BaseRepositoryTests : IDisposable
    {
        private readonly string _tableName = "user";
        private readonly MockRepository _mockRepo;
        private readonly BaseRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepositoryTests"/> class.
        /// </summary>
        /// <param name="output">test output helper.</param>
        public BaseRepositoryTests(ITestOutputHelper output)
        {
            _mockRepo = new MockRepository(MockBehavior.Strict);
            var iConfigMock = _mockRepo.Create<IConfiguration>();
            iConfigMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());
            _repository = new BaseRepository(iConfigMock.Object);
            _repository.DeleteAllAsync(_tableName).Wait();
            _repository.InsertAsync<User>(_tableName, new List<string>[]
            {
                new List<string> { "2", "Test User", "US", "11/14/2021 7:47:41 PM +00:00", "US-12345" },
            }).Wait();
        }

        ///// <summary>
        ///// Remove all items from the table.
        ///// </summary>
        public void Dispose()
        {
            _repository.DeleteAllAsync(_tableName).Wait();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Test to see if successfully get all table items
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task GetAllAsync_ValidInput_Success()
        {
            var result = await _repository.GetAllAsync<User>(_tableName);
            Assert.NotEmpty(result);
            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to make sure that exception is thrown on invalid input.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task GetAllAsync_InvalidInput_Exception()
        {
            Assert.ThrowsAny<Exception>(() => _repository.GetAllAsync<User>(string.Empty).Wait());
            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to see if successfully get specific table item
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task GetAsync_ValidInput_Success()
        {
            var result = await _repository.GetAsync<User>(_tableName, "UserId", "2");

            Assert.Equal("Test User", result.FullName);
            Assert.Equal("2", result.UserId);
            Assert.Equal("US", result.CountryCode.ToString());
            Assert.Equal("US-12345", result.PassportId);

            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to make sure that exception is thrown on invalid input.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task GetAsync_InvalidInput_Exception()
        {
            Assert.ThrowsAny<Exception>(() => _repository.GetAsync<User>("abc", string.Empty, "def").Wait());
            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to see if successfully insert new table item
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task InsertAsync_ValidInput_Success()
        {
            await _repository.InsertAsync<User>(_tableName, new List<string>[]
            {
                new List<string> { "3", "Test User 2", "CA", DateTime.UtcNow.ToString(), "US-23456" },
            });

            var insertedData = await _repository.GetAsync<User>(_tableName, "UserId", "3");
            Assert.NotNull(insertedData);

            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to make sure that exception is thrown on invalid input.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task InsertAsync_InvalidInput_Exception()
        {
            var emptyInput = new List<List<string>>();
            Assert.ThrowsAny<Exception>(() => _repository.InsertAsync<User>(string.Empty, emptyInput).Wait());
            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to see if successfully update table item
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task UpdateAsync_ValidInput_Success()
        {
            await _repository.UpdateAsync(_tableName, "UserId", "2", new Dictionary<string, string>
            {
                { "PassportId", "CA-12345" },
            });

            var updatedResult = await _repository.GetAsync<User>(_tableName, "UserId", "2");

            Assert.Equal("Test User", updatedResult.FullName);
            Assert.Equal("2", updatedResult.UserId);
            Assert.Equal("CA-12345", updatedResult.PassportId);

            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to make sure that exception is thrown on invalid input.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task UpdateAsync_InvalidInput_Exception()
        {
            var emptyInput = new List<List<string>>();
            Assert.ThrowsAny<Exception>(() => _repository.UpdateAsync(string.Empty, string.Empty, string.Empty, new Dictionary<string, string>()).Wait());
            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to see if successfully gets some from table
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task GetSomeAsync_ValidInput_Success()
        {
            var result = await _repository.GetSomeAsync<User>(_tableName, new Dictionary<string, string>()
            {
                { "UserId", "2" },
            });

            var retrievedUser = Assert.Single(result);
            Assert.Equal("2", retrievedUser.UserId);
            Assert.Equal("Test User", retrievedUser.FullName);
            Assert.Equal("US", retrievedUser.CountryCode);
            Assert.Equal("11/14/2021 7:47:41 PM +00:00", retrievedUser.CreatedAt.ToString());
            Assert.Equal("US-12345", retrievedUser.PassportId);

            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to make sure that exception is thrown on invalid input.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task GetSomeAsync_InvalidInput_Exception()
        {
            var emptyInput = new List<List<string>>();
            Assert.ThrowsAny<Exception>(() => _repository.GetSomeAsync<User>(string.Empty, null).Wait());
            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to see if successfully remove entry from table
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task DeleteAsync_ValidInput_Success()
        {
            await _repository.DeleteAsync<User>(_tableName, "UserId", "2");

            var nullResult = await _repository.GetAsync<User>(_tableName, "UserId", "2");

            Assert.Null(nullResult);

            _mockRepo.VerifyAll();
        }

        /// <summary>
        /// Test to make sure that exception is thrown on invalid input.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [IgnoreOnCiCdFact]
        public async Task DeleteAsync_InvalidInput_Exception()
        {
            var emptyInput = new List<List<string>>();
            Assert.ThrowsAny<Exception>(() => _repository.DeleteAsync<User>(string.Empty, string.Empty, string.Empty).Wait());
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
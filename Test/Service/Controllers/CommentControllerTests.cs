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
using Management.Repository;
using Management.Ports;

using Microsoft.AspNetCore.Mvc;

using Service.Controllers;

namespace Test.Service.Controllers
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
    public class CommentControllerTests : IDisposable
    {
        private readonly string _tableName = "comment";
        private readonly ITestOutputHelper _output;
        private readonly MockRepository _mockRepo;
        private readonly BaseRepository _baseRepository;
        private readonly CommentRepository _repository;
        private readonly CommentController _controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentControllerTests"/> class.
        /// </summary>
        /// <param name="output">test output helper.</param>
        public CommentControllerTests(ITestOutputHelper output)
        {
            this._output = output;
            _mockRepo = new MockRepository(MockBehavior.Strict);
            var iConfigMock = _mockRepo.Create<IConfiguration>();
            iConfigMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());
            _baseRepository = new BaseRepository(iConfigMock.Object);
            _repository = new CommentRepository(_baseRepository);
            _baseRepository.DeleteAllAsync(_tableName).Wait();
            _baseRepository.InsertAsync<Comment>(_tableName, new List<string>[]
            {
                new List<string> { "0", "Test User", "US", "TX", "11/14/2021 7:47:41 PM +00:00", "Nice place!" },
            }).Wait();
            _controller = new CommentController(new CommentPort(_repository));
        }

        ///// <summary>
        ///// Remove all items from the table.
        ///// </summary>
        public void Dispose()
        {
            _baseRepository.DeleteAllAsync(_tableName).Wait();
        }

        [IgnoreOnCiCdFact]
        public async Task GetCommentByLocation_RetrievedCommentSuccess()
        {
            var result = await _controller.GetCommentByLocation(null, "US", "TX");

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            _mockRepo.VerifyAll();
        }

        [IgnoreOnCiCdFact]
        public async Task GetCommentByLocation_InvalidCountryCode()
        {
            var result = await _controller.GetCommentByLocation(null, "AB", "NY");

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult.Value);
            _mockRepo.VerifyAll();
        }

        [IgnoreOnCiCdFact]
        public async Task GetCommentByLocation_InvalidStateCode()
        {
            var result = await _controller.GetCommentByLocation(null, "US", "ZZ");

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult.Value);
            _mockRepo.VerifyAll();
        }

        [IgnoreOnCiCdFact]
        public async Task GetCommentByLocation_InvalidCountryStateCode()
        {
            var result = await _controller.GetCommentByLocation(null, "CA", "NY");

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult.Value);
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
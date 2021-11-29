using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Management.Clients;
using Management.DomainModels;
using Management.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace Test.Management.Integration
{
    /// <summary>
    /// Integration test for covid data client
    /// </summary>
    public class CovidDataClientIntegrationTests
    {
        /// <summary>
        /// Only validate if request can reach the vendor and have success result. 
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [Fact]
        public async Task GetRequest_ValidData_CanReceiveSuccessResponse()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var configMock = mockRepo.Create<IConfiguration>();

            configMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());

            var covidClient = new CovidDataClient(configMock.Object);

            var resultME = await covidClient.CalculateScoreForStateAsync(State.Wrap("ME"), CountryCode.US, CancellationToken.None);
            Assert.Equal(100, resultME.Item2);

            var resultWA = await covidClient.CalculateScoreForStateAsync(State.Wrap("WA"), CountryCode.US, CancellationToken.None);
            Assert.Equal(80, resultWA.Item2);

            var resultVA = await covidClient.CalculateScoreForStateAsync(State.Wrap("VA"), CountryCode.US, CancellationToken.None);
            Assert.Equal(60, resultVA.Item2);

            var resultNC = await covidClient.CalculateScoreForStateAsync(State.Wrap("NC"), CountryCode.US, CancellationToken.None);
            Assert.Equal(40, resultNC.Item2);

            var resultNY = await covidClient.CalculateScoreForStateAsync(State.Wrap("NY"), CountryCode.US, CancellationToken.None);
            Assert.Equal(20, resultNY.Item2);

            var resultCA = await covidClient.CalculateScoreForStateAsync(State.Wrap("CA"), CountryCode.US, CancellationToken.None);
            Assert.Equal(0, resultCA.Item2);

            var exception = await Assert.ThrowsAsync<Exception>(() => covidClient.GetStateCovidDataAsync(State.Wrap("abc"), CancellationToken.None));
        }

        public class Section : IConfigurationSection
        {
            public string this[string key] { get => ""; set => throw new NotImplementedException(); }

            /// <summary>
            /// Gets connection string 
            /// </summary>
            public string Key => "ConnectionStrings";

            /// <summary>
            /// Gets connection path
            /// </summary>
            public string Path => throw new NotImplementedException();

            /// <summary>
            /// Gets or sets server config
            /// </summary>
            public string Value { get => ""; set => throw new NotImplementedException(); }

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
}

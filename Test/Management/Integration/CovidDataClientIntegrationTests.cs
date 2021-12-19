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
            //var mockRepo = new MockRepository(MockBehavior.Strict);
            //var configMock = mockRepo.Create<IConfiguration>();

            //configMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());

            var covidClient = new CovidDataClient();

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

        /// <summary>
        /// Representation of configuration section
        /// </summary>
        public class Section : IConfigurationSection
        {
            /// <summary>
            /// Gets api key
            /// </summary>
            /// <param name="key">api key string.</param>
            public string this[string key] { get => string.Empty; set => throw new NotImplementedException(); }

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
            public string Value { get => string.Empty; set => throw new NotImplementedException(); }

            /// <summary>
            /// Gets the immediate descendant configuration sub-sections.
            /// </summary>
            /// <returns>IConfigurationSection.</returns>
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
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Management.Clients;
using Management.Clients.Models;
using Management.DomainModels;
using Management.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace Test.Management.Integration
{
    /// <summary>
    /// Integration test for weather data client
    /// </summary>
    public class WeatherDataClientIntegrationTests
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

            var weatherClient = new WeatherDataClient(configMock.Object);

            var resultME = await weatherClient.CalculateScoreForStateAsync(State.Wrap("ME"), CountryCode.US, CancellationToken.None);
            Assert.Equal("ME", resultME.Item1.Value);

            var resultWA = await weatherClient.CalculateScoreForStateAsync(State.Wrap("WA"), CountryCode.US, CancellationToken.None);
            Assert.Equal("WA", resultWA.Item1.Value);

            var resultVA = await weatherClient.CalculateScoreForStateAsync(State.Wrap("VA"), CountryCode.US, CancellationToken.None);
            Assert.Equal("VA", resultVA.Item1.Value);

            var resultNC = await weatherClient.CalculateScoreForStateAsync(State.Wrap("NC"), CountryCode.US, CancellationToken.None);
            Assert.Equal("NC", resultNC.Item1.Value);

            var resultNY = await weatherClient.CalculateScoreForStateAsync(State.Wrap("NY"), CountryCode.US, CancellationToken.None);
            Assert.Equal("NY", resultNY.Item1.Value);

            var resultCA = await weatherClient.CalculateScoreForStateAsync(State.Wrap("CA"), CountryCode.US, CancellationToken.None);
            Assert.Equal("CA", resultCA.Item1.Value);

            var exception = await Assert.ThrowsAsync<Exception>(() => weatherClient.GetCityWeatherDataAsync(City.Wrap("San Diegos"), State.Wrap("CA"), CountryCode.US, CancellationToken.None));
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
            public string this[string key] { get => "a48028d4346f4094f5450108adf3c73e"; set => throw new NotImplementedException(); }

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
            public string Value { get => "a48028d4346f4094f5450108adf3c73e"; set => throw new NotImplementedException(); }

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
}

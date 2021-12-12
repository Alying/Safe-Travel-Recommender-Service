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
    /// Integration test for air quality data client
    /// </summary>
    public class AirQualityDataClientIntegrationTests
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

            var airClient = new AirQualityDataClient(configMock.Object);

            //var exception = await Assert.ThrowsAsync<Exception>(() => airClient.GetCityAirQualityDataAsync(City.Wrap("ase"), State.Wrap("California"), CountryCode.US, CancellationToken.None));

            var resultME = await airClient.CalculateScoreForStateAsync(State.Wrap("ME"), CountryCode.US, CancellationToken.None);
            Assert.Equal("Maine", resultME.Item1.Value);
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
            public string this[string key] { get => "ecc93ce2-d18c-44a3-a414-adc270da84bd"; set => throw new NotImplementedException(); }
            
            //public string this[string key] { get => "9ac310f1-7fb7-4ced-8808-478fdbe6ed13"; set => throw new NotImplementedException(); }
            
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
            public string Value { get => "ecc93ce2-d18c-44a3-a414-adc270da84bd"; set => throw new NotImplementedException(); }
            
            //public string Value { get => "9ac310f1-7fb7-4ced-8808-478fdbe6ed13"; set => throw new NotImplementedException(); }

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

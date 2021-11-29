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

            var exception = await Assert.ThrowsAsync<Exception>(() => airClient.GetCityAirQualityDataAsync(City.Wrap("ase"), State.Wrap("California"), CountryCode.US, CancellationToken.None));

            var resultME = await airClient.CalculateScoreForStateAsync(State.Wrap("ME"), CountryCode.US, CancellationToken.None);
            Assert.Equal("ME", resultME.Item1.Value);

            var resultWA = await airClient.CalculateScoreForStateAsync(State.Wrap("WA"), CountryCode.US, CancellationToken.None);
            Assert.Equal("WA", resultWA.Item1.Value);

            var resultVA = await airClient.CalculateScoreForStateAsync(State.Wrap("VA"), CountryCode.US, CancellationToken.None);
            Assert.Equal("VA", resultVA.Item1.Value);

            var resultNC = await airClient.CalculateScoreForStateAsync(State.Wrap("NC"), CountryCode.US, CancellationToken.None);
            Assert.Equal("NC", resultNC.Item1.Value);

            var resultNY = await airClient.CalculateScoreForStateAsync(State.Wrap("NY"), CountryCode.US, CancellationToken.None);
            Assert.Equal("NY", resultNY.Item1.Value);

            var resultCA = await airClient.CalculateScoreForStateAsync(State.Wrap("CA"), CountryCode.US, CancellationToken.None);
            Assert.Equal("CA", resultCA.Item1.Value);
        }

        public class Section : IConfigurationSection
        {
            //public string this[string key] { get => "ecc93ce2-d18c-44a3-a414-adc270da84bd"; set => throw new NotImplementedException(); }
            public string this[string key] { get => "9ac310f1-7fb7-4ced-8808-478fdbe6ed13"; set => throw new NotImplementedException(); }
            
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
            //public string Value { get => "ecc93ce2-d18c-44a3-a414-adc270da84bd"; set => throw new NotImplementedException(); }
            public string Value { get => "9ac310f1-7fb7-4ced-8808-478fdbe6ed13"; set => throw new NotImplementedException(); }

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

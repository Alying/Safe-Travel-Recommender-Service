using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Management;
using Management.DomainModels;
using Management.Enum;
using Management.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace Test.Management
{
    /// <summary>
    /// Unit tests for decision engine
    /// </summary>
    public class DecisionEngineTests
    {
        [Fact]
        public async Task GetDefaultRecommendationAsync_AllGood_ReturnData()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);

            var mockCovid = mockRepo.Create<ICovidDataClient>();
            mockCovid.Setup(client => client.CalculateScoreForStateAsync(
                                                It.IsAny<State>(),
                                                It.IsAny<CountryCode>(),
                                                It.IsAny<CancellationToken>())).Returns(Task.FromResult((State.Wrap("CA"), 50.0)));

            var mockWeather = mockRepo.Create<IWeatherDataClient>();
            mockWeather.Setup(client => client.CalculateScoreForStateAsync(
                                               It.IsAny<State>(),
                                               It.IsAny<CountryCode>(),
                                               It.IsAny<CancellationToken>())).Returns(Task.FromResult((State.Wrap("CA"), 50.0)));

            var mockAir = mockRepo.Create<IAirQualityDataClient>();
            mockAir.Setup(client => client.CalculateScoreForStateAsync(
                                               It.IsAny<State>(),
                                               It.IsAny<CountryCode>(),
                                               It.IsAny<CancellationToken>())).Returns(Task.FromResult((State.Wrap("CA"), 50.0)));

            var decisionEngine = new DecisionEngine(mockCovid.Object, mockWeather.Object, mockAir.Object);
            var result = await decisionEngine.GetDefaultCountryRecommendationAsync(CountryCode.US, CancellationToken.None);

            var resultType = Assert.IsType<List<Recommendation>>(result);
            Assert.NotNull(resultType[0]);
        }
    }
}
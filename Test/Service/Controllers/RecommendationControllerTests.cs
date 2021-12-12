using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Service.Controllers;
using Management.DomainModels;
using Management.Enum;
using Management.Interface;
using Management.Ports;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Test.Service.Controllers
{
    /// <summary>
    /// Tests for recommendation controller.
    /// </summary>
    public class RecommendationControllerTests
    {
        [Fact]
        public async Task GetDefaultRecommendations_AllGood_ReturnData()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var mockEngine = mockRepo.Create<IDecisionEngine>();

            var expected = new List<Recommendation>();
            expected.Add(new Recommendation(
                                            CountryCode.US,
                                            State.Wrap("CA"),
                                            RecommendationState.Not_Recommended,
                                            45.3,
                                            100,
                                            0,
                                            51));

            mockEngine.Setup(engine => engine.GetDefaultCountryRecommendationAsync(
                                        It.IsAny<CountryCode>(),
                                        CancellationToken.None)).Returns(Task.FromResult<IEnumerable<Recommendation>>(expected));

            var controller = new RecommendationController(new RecommendationPort(mockEngine.Object));
            var result = await controller.GetTopRecommendations(CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetRecommendations__CountryCode_AllGood_ReturnData()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var mockEngine = mockRepo.Create<IDecisionEngine>();

            var expected = new List<Recommendation>();
            expected.Add(new Recommendation(
                                            CountryCode.US,
                                            State.Wrap("CA"),
                                            RecommendationState.Not_Recommended,
                                            45.3,
                                            100,
                                            0,
                                            51));

            mockEngine.Setup(engine => engine.GetDefaultCountryRecommendationAsync(
                                        It.IsAny<CountryCode>(),
                                        CancellationToken.None)).Returns(Task.FromResult<IEnumerable<Recommendation>>(expected));

            var controller = new RecommendationController(new RecommendationPort(mockEngine.Object));
            var result = await controller.GetRecommendationByCountryCode("US", CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Theory]
        [InlineData("hello")]
        [InlineData(null)]
        public async Task GetRecommendations__CountryCodeNotValid_ReturnData(string country)
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var mockEngine = mockRepo.Create<IDecisionEngine>();

            var controller = new RecommendationController(new RecommendationPort(mockEngine.Object));
            var result = await controller.GetRecommendationByCountryCode(country, CancellationToken.None);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task GetLocationDetail_CountryCodeStateValid_ReturnData()
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var mockEngine = mockRepo.Create<IDecisionEngine>();

            mockEngine.Setup(engine => engine.GetStateInfoAsync(
                                                    It.IsAny<CountryCode>(),
                                                    It.IsAny<State>(),
                                                    CancellationToken.None)).ReturnsAsync(
                                                                                          new Recommendation(
                                                                                          CountryCode.US,
                                                                                          State.Wrap("CA"),
                                                                                          RecommendationState.Not_Recommended,
                                                                                          45.3,
                                                                                          100,
                                                                                          0,
                                                                                          51));

            var controller = new RecommendationController(new RecommendationPort(mockEngine.Object));
            var result = await controller.GetRecommendationByCountryCodeAndStateCode("US", "CA", CancellationToken.None);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Theory]
        [InlineData("jdhafsklfjhasdk", "California")]
        [InlineData(null, "099090900")]
        [InlineData("US", null)]
        public async Task GetLocationDetail_CountryCodeStateNotValid_ReturnData(string country, string state)
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var mockEngine = mockRepo.Create<IDecisionEngine>();

            var controller = new RecommendationController(new RecommendationPort(mockEngine.Object));
            var result = await controller.GetRecommendationByCountryCodeAndStateCode(country, state, CancellationToken.None);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.NotNull(notFoundResult.Value);
        }
    }
}

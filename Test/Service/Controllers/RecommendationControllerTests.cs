using Management.DomainModels;
using Management.Enum;
using Management.Interface;
using Management.Ports;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using Service.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Test.Service.Controllers
{
    public class RecommendationControllerTests
    {
        //[Fact]
        //public async Task GetDefaultRecommendations_AllGood_ReturnData()
        //{
        //    var mockRepo = new MockRepository(MockBehavior.Strict);
        //    var mockEngine = mockRepo.Create<IDecisionEngine>();
        //    mockEngine
        //        .Setup(engine => engine.GetDefaultStateRecommendationAsync(It.IsAny<State>(), It.IsAny<CountryCode>(), CancellationToken.None))
        //        .ReturnsAsync(new Dictionary<City, double> 
        //        {
        //            { City.Wrap("Test"), 90 }
        //        });

        //    var controller = new RecommendationController(new RecommendationPort(mockEngine.Object));
        //    var result =  await controller.GetTopRecommendations(CancellationToken.None);

        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.NotNull(okResult.Value);
        //}

        //[Fact]
        //public async Task GetLocationDetail_CountryCodeStateValid_ReturnData()
        //{
        //    var mockRepo = new MockRepository(MockBehavior.Strict);
        //    var mockEngine = mockRepo.Create<IDecisionEngine>();

        //    var controller = new RecommendationController(new RecommendationPort(mockEngine.Object));
        //    var result = await controller.GetRecommendationByCountryCodeAndStateCode("US", "California", CancellationToken.None);

        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.NotNull(okResult.Value);
        //}

        //[Theory]
        //[InlineData("jdhafsklfjhasdk", "California")]
        //[InlineData(null, "099090900")]
        //[InlineData("US", null)]
        //public async Task GetLocationDetail_CountryCodeStateNotValid_ReturnData(string country, string state)
        //{
        //    var mockRepo = new MockRepository(MockBehavior.Strict);
        //    var mockEngine = mockRepo.Create<IDecisionEngine>();

        //    var controller = new RecommendationController(new RecommendationPort(mockEngine.Object));
        //    var result = await controller.GetRecommendationByCountryCodeAndStateCode(country, state, CancellationToken.None);

        //    var okResult = Assert.IsType<NotFoundObjectResult>(result);
        //    Assert.NotNull(okResult.Value);
        //}
    }
}

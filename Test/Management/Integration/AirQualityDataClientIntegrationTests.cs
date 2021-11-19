// <copyright file="AirQualityDataClientTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    public class AirQualityDataClientIntegrationTests
    {
        // Only validate if request can reach the vendor and have success result. 
        [Fact]
        public async Task GetRequest_ValidData_CanReceiveSuccessResponse() 
        {
            var mockRepo = new MockRepository(MockBehavior.Strict);
            var configMock = mockRepo.Create<IConfiguration>();

            configMock.Setup(config => config.GetSection(It.IsAny<string>())).Returns(new Section());

            var airClient = new AirQualityDataClient(configMock.Object);

            var result = await airClient.GetAirQualityAsync(City.Wrap("Los Angeles"), State.Wrap("California"), CountryCode.US, CancellationToken.None);

            Assert.Equal("success", result.Status);
        }

        public class Section : IConfigurationSection
        {
            public string this[string key] { get => "ecc93ce2-d18c-44a3-a414-adc270da84bd"; set => throw new NotImplementedException(); }

            public string Key => "ConnectionStrings";

            public string Path => throw new NotImplementedException();

            public string Value { get => "ecc93ce2-d18c-44a3-a414-adc270da84bd"; set => throw new NotImplementedException(); }

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

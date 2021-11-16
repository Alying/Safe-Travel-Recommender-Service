// <copyright file="DecisionEngine.cs" company="ASE#">
//     Copyright (c) ASE#. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Interface;

namespace Management
{
    public class DecisionEngine : IDecisionEngine
    {
        private readonly ICovidDataClient _covidDataClient;
        private readonly IWeatherDataClient _wheatherDataClient;
        private readonly IAirQualityDataClient _airQualityDataClient;

        public DecisionEngine(
            ICovidDataClient covidDataClient,
            IWeatherDataClient wheatherDataClient,
            IAirQualityDataClient airQualityDataClient)
        {
            _covidDataClient = covidDataClient ?? throw new ArgumentNullException(nameof(covidDataClient));
            _wheatherDataClient = wheatherDataClient ?? throw new ArgumentNullException(nameof(covidDataClient));
            _airQualityDataClient = airQualityDataClient ?? throw new ArgumentNullException(nameof(airQualityDataClient));
        }

        public Task<IEnumerable<Recommendation>> CalculateDesiredLocationAsync()
        {
            var result = (0.3 * _covidDataClient.CalculateScoreAsync()) + (0.3 * _wheatherDataClient.CalculateScoreAsync()) + (0.3 * _airQualityDataClient.CalculateScoreAsync());

            return Task.FromResult(Enumerable.Empty<Recommendation>());
        }

        /// <summary>
        /// Gets the specific location's information 
        /// </summary>
        /// <param name="location">The country and state the user inquired.</param>
        /// <param name="userId">The user's unique id.</param>
        /// <returns>The state's information.</returns>
        public async Task<Recommendation> GetSpecificLocationInfoAsync(Location location, UserId userId)
        {
            var stateInfo = new Recommendation(
                                location,
                                userId,
                                new CovidData(1681169, 39029),
                                new WeatherData("Expect showers today", 40, 48),
                                new AirQualityData(41, 62, 2));
            return stateInfo;
        }
    }
}

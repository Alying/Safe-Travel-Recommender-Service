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
    /// <summary>
    /// Calculate the top 10 travel recommendations using weighted score
    /// and get specific state's travel information
    /// </summary>
    public class DecisionEngine : IDecisionEngine
    {
        private readonly ICovidDataClient _covidDataClient;
        private readonly IWeatherDataClient _wheatherDataClient;
        private readonly IAirQualityDataClient _airQualityDataClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="DecisionEngine"/> class.
        /// </summary>
        /// <param name="covidDataClient">The COVID-19 data client.</param>
        /// <param name="weatherDataClient">The weather data client.</param>
        /// <param name="airQualityDataClient">The air quality data client.</param>
        public DecisionEngine(
            ICovidDataClient covidDataClient,
            IWeatherDataClient wheatherDataClient,
            IAirQualityDataClient airQualityDataClient)
        {
            _covidDataClient = covidDataClient ?? throw new ArgumentNullException(nameof(covidDataClient));
            _wheatherDataClient = wheatherDataClient ?? throw new ArgumentNullException(nameof(covidDataClient));
            _airQualityDataClient = airQualityDataClient ?? throw new ArgumentNullException(nameof(airQualityDataClient));
        }

        /// <summary>
        /// Calculate the desired location using weighted scores from COVID-19, weather, and air quality
        /// </summary>
        /// <returns>The weighted score.</returns>
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

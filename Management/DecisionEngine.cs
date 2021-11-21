// <copyright file="DecisionEngine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Enum;
using Management.Interface;

namespace Management
{
    /// <summary>
    /// Calculate the top 10 travel recommendations using weighted score
    /// and get specific state's travel information.
    /// </summary>
    public class DecisionEngine : IDecisionEngine
    {
        private const double AirQualityWeight = 0.3;
        private const double CovidDataWeight = 0.4;
        private const double WeatherDataWeight = 0.3;

        private readonly List<State> _defaultUsStates;
        private readonly List<State> _defaultCaStates;

        private readonly ICovidDataClient _covidDataClient;
        private readonly IWeatherDataClient _weatherDataClient;
        private readonly IAirQualityDataClient _airQualityDataClient;
        private readonly List<IWeightedClient> _clients;

        /// <summary>
        /// Initializes a new instance of the <see cref="DecisionEngine"/> class.
        /// </summary>
        /// <param name="covidDataClient">The COVID-19 data client.</param>
        /// <param name="weatherDataClient">The weather data client.</param>
        /// <param name="airQualityDataClient">The air quality data client.</param>
        public DecisionEngine(
            ICovidDataClient covidDataClient,
            IWeatherDataClient weatherDataClient,
            IAirQualityDataClient airQualityDataClient)
        {
            _covidDataClient = covidDataClient ?? throw new ArgumentNullException(nameof(covidDataClient));
            _weatherDataClient = weatherDataClient ?? throw new ArgumentNullException(nameof(covidDataClient));
            _airQualityDataClient = airQualityDataClient ?? throw new ArgumentNullException(nameof(airQualityDataClient));
            _clients = new List<IWeightedClient>
            {
                _covidDataClient,
                _weatherDataClient,
                _airQualityDataClient,
            };

            // Temp solution for demo purpose
            // TODO: Add static mapping for State - cities
            // For now hardcode 2 cities for California
            _defaultUsStates = new List<State>
            {
                State.Wrap("Massachusetts"),
                State.Wrap("Georgia"),
                State.Wrap("California"),
                State.Wrap("Illinois"),
                State.Wrap("Washington"),
            };

            _defaultCaStates = new List<State>
            {
                State.Wrap("Manitoba"),
                State.Wrap("Alberta"),
                State.Wrap("Ontario"),
                State.Wrap("Quebec"),
                State.Wrap("Saskatchewan"),
            };
        }

        /// <summary>
        /// Calculate the desired location using weighted scores from COVID-19, weather, and air quality.
        /// </summary>
        /// <param name="countryCode">country code eg. "US".</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>The weighted score.</returns>
        public async Task<Dictionary<State, double>> GetDefaultCountryRecommendationAsync(
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var airBag = new ConcurrentBag<(State state, double score)>();
            var weatherBag = new ConcurrentBag<(State state, double score)>();
            var covidBag = new ConcurrentBag<(State state, double score)>();

            var states = countryCode == CountryCode.US ? _defaultUsStates : _defaultCaStates;

            var stateTasks = states.Select(async state =>
            {
                airBag.Add(await _airQualityDataClient.CalculateScoreForStateAsync(state, countryCode, cancellationToken));

                weatherBag.Add(await _weatherDataClient.CalculateScoreForStateAsync(state, countryCode, cancellationToken));

                covidBag.Add(await _covidDataClient.CalculateScoreForStateAsync(state, countryCode, cancellationToken));
            });

            await Task.WhenAll(stateTasks);

            var airResultLookup = airBag.ToDictionary(b => b.state, b => b.score);
            var weatherResultLookup = airBag.ToDictionary(b => b.state, b => b.score);
            var covidResultLookup = airBag.ToDictionary(b => b.state, b => b.score);

            var resultDictionary = new Dictionary<State, double>();

            foreach (var res in airResultLookup)
            {
                var finalScore
                    = (AirQualityWeight * res.Value)
                    + (WeatherDataWeight * weatherResultLookup[res.Key])
                    + (CovidDataWeight * covidResultLookup[res.Key]);

                resultDictionary.Add(res.Key, finalScore);
            }

            return resultDictionary;
        }

        /// <summary>
        /// Gets the specific location's information.
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

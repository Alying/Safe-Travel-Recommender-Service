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

            // Currently only implemented US
            _defaultUsStates = new List<State>
            {
                State.Wrap("CA"),
                State.Wrap("FL"),
                State.Wrap("GA"),
                State.Wrap("IL"),
                State.Wrap("MA"),
                State.Wrap("MD"),
                State.Wrap("NC"),
                State.Wrap("NJ"),
                State.Wrap("NY"),
                State.Wrap("WA"),
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
        public async Task<IEnumerable<Recommendation>> GetDefaultCountryRecommendationAsync(
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var recommendationBag = new ConcurrentBag<Recommendation>();

            var states = countryCode == CountryCode.US ? _defaultUsStates : _defaultCaStates;

            var stateTasks = states.Select(async state =>
            {
                recommendationBag.Add(await GetStateInfoAsync(countryCode, state, cancellationToken));
            });

            await Task.WhenAll(stateTasks);

            return recommendationBag.OrderByDescending(r => r.OverallScore).ToList();
        }

        /// <summary>
        /// Gets the specific location's information.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <param name="state">The state for specific country</param>
        /// /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>The state's information.</returns>
        public async Task<Recommendation> GetStateInfoAsync(CountryCode countryCode, State state, CancellationToken cancellationToken)
        {
            var airScore = await _airQualityDataClient.CalculateScoreForStateAsync(state, countryCode, cancellationToken);
            var covidScore = await _covidDataClient.CalculateScoreForStateAsync(state, countryCode, cancellationToken);
            var weatherScore = await _weatherDataClient.CalculateScoreForStateAsync(state, countryCode, cancellationToken);

            var finalScore = Math.Round(
                                         (AirQualityWeight * airScore.score) +
                                         (CovidDataWeight * covidScore.score) +
                                         (WeatherDataWeight * weatherScore.score), 1);

            return new Recommendation(
                countryCode: countryCode,
                state: state,
                recommendationState: GetRecommendationState(finalScore),
                overallScore: finalScore,
                airQualityScore: airScore.score,
                covidIndexScore: covidScore.score,
                weatherScore: weatherScore.score);
        }

        /// <summary>
        /// Determines the score for the state.
        /// </summary>
        /// <param name="finalScore">The final score of the state.</param>
        /// <returns>The state's level of recommendation.</returns>
        private static RecommendationState GetRecommendationState(double finalScore)
        {
            if (finalScore >= 80 && finalScore <= 100)
            {
                return RecommendationState.Highly_Recommended;
            }

            if (finalScore >= 60 && finalScore < 80)
            {
                return RecommendationState.Recommended;
            }

            return RecommendationState.Not_Recommended;
        }
    }
}

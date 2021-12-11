using System;
using Management.Enum;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a recommendation (basic information/top 10 recommendations) in the safe-travel service.
    /// </summary>
    public class Recommendation
    {
        /// <summary>
        /// Gets the country
        /// </summary>
        public CountryCode CountryCode { get; }

        /// <summary>
        /// Gets the state
        /// </summary>
        public State State { get; }

        /// <summary>
        /// Gets the recommendation state/level
        /// </summary>
        public RecommendationState RecommendationState { get; }

        /// <summary>
        /// Gets the overall score
        /// </summary>
        public double OverallScore { get; }

        /// <summary>
        /// Gets the air quality score
        /// </summary>
        public double AirQualityScore { get; }

        /// <summary>
        /// Gets the covid score
        /// </summary>
        public double CovidIndexScore { get; }

        /// <summary>
        /// Gets the weather score
        /// </summary>
        public double WeatherScore { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recommendation"/> class.
        /// </summary>
        /// <param name="countryCode">country of interest eg. US</param>
        /// <param name="state">state of interest eg. NY</param>
        /// <param name="recommendationState">recommendation level/state</param>
        /// <param name="overallScore">timestamp of the comment</param>
        /// <param name="airQualityScore">air quality score</param>
        /// <param name="covidIndexScore">covid score</param>
        /// <param name="weatherScore">weather score</param>
        public Recommendation(
            CountryCode countryCode,
            State state,
            RecommendationState recommendationState,
            double overallScore,
            double airQualityScore,
            double covidIndexScore,
            double weatherScore)
        {
            CountryCode = countryCode;
            State = state ?? throw new ArgumentNullException(nameof(state));
            RecommendationState = recommendationState;
            OverallScore = overallScore;
            AirQualityScore = airQualityScore;
            CovidIndexScore = covidIndexScore;
            WeatherScore = weatherScore;
        }
    }
}

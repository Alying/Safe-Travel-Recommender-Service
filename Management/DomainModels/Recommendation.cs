using Management.Enum;
using System;

namespace Management.DomainModels
{
    public class Recommendation
    {
        public CountryCode CountryCode { get; }

        public State State { get; }

        public RecommendationState RecommendationState { get; }

        public double OverallScore { get; }

        public double AirQualityScore { get; }

        public double CovidIndexScore { get; }

        public double WeatherScore { get; }

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

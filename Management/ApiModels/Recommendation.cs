using Management.Enum;

namespace Management.ApiModels
{
    public class Recommendation
    {
        public CountryCode CountryCode { get; set; }

        public string State { get; set; }

        public RecommendationState RecommendationState { get; set; }

        public double OverallScore { get; set; }

        public double AirQualityScore { get; set; }

        public double CovidIndexScore { get; set; }

        public double WeatherScore { get; set; }
    }
}

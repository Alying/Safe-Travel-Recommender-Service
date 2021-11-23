using Management.Enum;

namespace Management.ApiModels
{
    public class Recommendation
    {
        public string CountryCode { get; set; }

        public string State { get; set; }

        public string RecommendationState { get; set; }

        public double OverallScore { get; set; }

        public double AirQualityScore { get; set; }

        public double CovidIndexScore { get; set; }

        public double WeatherScore { get; set; }
    }
}

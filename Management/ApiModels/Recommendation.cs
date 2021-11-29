namespace Management.ApiModels
{
    /// <summary>
    /// Representation of a recommendation (basic information/top 10 recommendations) in the safe-travel service.
    /// </summary>
    public class Recommendation
    {
        /// <summary>
        /// Gets or sets country
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets state
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets level of recommendation
        /// </summary>
        public string RecommendationState { get; set; }

        /// <summary>
        /// Gets or sets overall score of a state
        /// </summary>
        public double OverallScore { get; set; }

        /// <summary>
        /// Gets or sets a state's air quality score
        /// </summary>
        public double AirQualityScore { get; set; }

        /// <summary>
        /// Gets or sets a state's covid score
        /// </summary>
        public double CovidIndexScore { get; set; }

        /// <summary>
        /// Gets or sets a state's weather score
        /// </summary>
        public double WeatherScore { get; set; }
    }
}

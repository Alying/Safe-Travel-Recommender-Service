using Newtonsoft.Json;

namespace Management.Clients.Models
{
    /// <summary>
    /// Get and set responses from air quality API
    /// </summary>
    public class AirQualityCityResponse
    {
        /// <summary>
        /// Gets or sets status from air quality api call
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets data grabbed from air quality api call
        /// </summary>
        [JsonProperty("data")]
        public AirQualityData AirQualityData { get; set; }
    }

    /// <summary>
    /// Gets or sets location and air quality forecast data
    /// </summary>
    public class AirQualityData
    {
        /// <summary>
        /// Gets or sets current air quality data
        /// </summary>
        [JsonProperty("current")]
        public Current Current { get; set; }
    }

    /// <summary>
    /// Gets or sets current air quality data
    /// </summary>
    public class Current
    {
        /// <summary>
        /// Gets or sets location of interest's current pollution data
        /// </summary>
        [JsonProperty("pollution")]
        public Pollution Pollution { get; set; }
    }

    /// <summary>
    /// Gets or sets location of interest's current pollution data
    /// </summary>
    public class Pollution
    {
        /// <summary>
        /// Gets or sets timestamp of when the air quality data was recorded
        /// </summary>
        [JsonProperty("ts")]
        public string Ts { get; set; }

        /// <summary>
        /// Gets or sets air quality index value based on United State's EPA standard
        /// </summary>
        [JsonProperty("aqius")]
        public double Aqius { get; set; }

        /// <summary>
        /// Gets or sets main pollutant for United States's air quality index
        /// </summary>
        [JsonProperty("mainus")]
        public string Mainus { get; set; }
    }
}

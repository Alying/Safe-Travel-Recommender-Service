using Newtonsoft.Json;

namespace Management.Clients.Models
{
    /// <summary>
    /// Get and set responses from weather API
    /// </summary>
    public class WeatherCityResponse
    {
        /// <summary>
        /// Gets or sets data grabbed from weather api call
        /// </summary>
        [JsonProperty("main")]
        public WeatherData WeatherData { get; set; }
    }

    /// <summary>
    /// Gets or sets current weather data
    /// </summary>
    public class WeatherData
    {
        /// <summary>
        /// Gets or sets temperature in Kelvin
        /// </summary>
        [JsonProperty("temp")]
        public double Temp { get; set; }

        /// <summary>
        /// Gets or sets atmospheric pressure in hPa
        /// </summary>
        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        /// <summary>
        /// Gets or sets humidity percentage
        /// </summary>
        [JsonProperty("humidity")]
        public double Humidity { get; set; }
    }
}

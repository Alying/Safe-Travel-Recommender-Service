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
        public Data Data { get; set; }
    }

    /// <summary>
    /// Gets or sets location and air quality forecast data
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Gets or sets city of interest
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets state of interest
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets country of interest
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets location of interest (country, state)
        /// </summary>
        [JsonIgnore]
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets air quality forecasts
        /// </summary>
        [JsonIgnore]
        public Forecasts Forecasts { get; set; }

        /// <summary>
        /// Gets or sets current air quality data
        /// </summary>
        [JsonProperty("current")]
        public Current Current { get; set; }
    }

    /// <summary>
    /// Gets or sets location's air quality information
    /// </summary>
    public class Location
    {
    }

    /// <summary>
    /// Gets or sets air quality forecasts information
    /// </summary>
    public class Forecasts
    {
    }

    /// <summary>
    /// Gets or sets current air quality data
    /// </summary>
    public class Current
    {
        /// <summary>
        /// Gets or sets location of interest's current weather data
        /// </summary>
        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        /// <summary>
        /// Gets or sets location of interest's current pollution data
        /// </summary>
        [JsonProperty("pollution")]
        public Pollution Pollution { get; set; }
    }

    /// <summary>
    /// Gets or sets location of interest's current weather data
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// Gets or sets timestamp of when the air quality data was recorded
        /// </summary>
        [JsonProperty("ts")]
        public string Ts { get; set; }

        /// <summary>
        /// Gets or sets temperature in Celsius
        /// </summary>
        [JsonProperty("tp")]
        public double Tp { get; set; }

        /// <summary>
        /// Gets or sets atmospheric pressure in hPa
        /// </summary>
        [JsonProperty("pr")]
        public double Pr { get; set; }

        /// <summary>
        /// Gets or sets humidity percentage
        /// </summary>
        [JsonProperty("hu")]
        public double Hu { get; set; }

        /// <summary>
        /// Gets or sets wind speed (m/s)
        /// </summary>
        [JsonProperty("ws")]
        public double Ws { get; set; }

        /// <summary>
        /// Gets or sets wind direction
        /// </summary>
        [JsonProperty("wd")]
        public double Wd { get; set; }
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

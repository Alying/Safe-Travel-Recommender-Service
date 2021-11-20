using System.Collections.Generic;
using Newtonsoft.Json;

namespace Management.Clients.Models
{
    /// <summary>
    /// Gets or sets city response (status and data) from air quality api call 
    /// </summary>
    public class AirQualityQueryCityResponse
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
        public List<CityData> Data { get; set; }
    }

    /// <summary>
    /// Gets or sets city data grabbed from air quality api call
    /// </summary>
    public class CityData
    {
        /// <summary>
        /// Gets or sets city name
        /// </summary>
        [JsonProperty("city")]
        public string CityName { get; set; }
    }
}

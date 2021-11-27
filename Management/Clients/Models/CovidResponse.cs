using Newtonsoft.Json;
using System.Collections.Generic;

namespace Management.Clients.Models
{
    /// <summary>
    /// Get and set responses from covid API
    /// </summary>
    public class CovidResponse
    {
        /// <summary>
        /// Gets or sets data grabbed from covid api call
        /// </summary>
        [JsonProperty("data")]
        public List<CovidData> CovidDataList { get; set; }
    }

    /// <summary>
    /// Gets or sets covid data
    /// </summary>
    public class CovidData
    {
        /// <summary>
        /// Gets or sets covid cases
        /// </summary>
        [JsonProperty("cases")]
        public Cases Cases { get; set; }
    }

    /// <summary>
    /// Gets or sets covid cases
    /// </summary>
    public class Cases
    {
        /// <summary>
        /// Gets or sets covid confirmed cases
        /// </summary>
        [JsonProperty("confirmed")]
        public Confirmed Confirmed { get; set; }
    }

    /// <summary>
    /// Gets or sets covid confirmed cases
    /// </summary>
    public class Confirmed
    {
        /// <summary>
        /// Gets or sets covid confirmed cases value
        /// </summary>
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}

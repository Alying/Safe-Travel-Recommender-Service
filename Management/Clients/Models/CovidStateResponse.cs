using Newtonsoft.Json;
using System.Collections.Generic;

namespace Management.Clients.Models
{
    /// <summary>
    /// Get and set responses from covid API
    /// </summary>
    public class CovidStateResponse
    {
        /// <summary>
        /// Gets or sets data grabbed from covid api call
        /// </summary>
        [JsonProperty("data")]
        public CovidData CovidData { get; set; }
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
        /// Gets or sets covid total cases
        /// </summary>
        [JsonProperty("total")]
        public Total Total { get; set; }
    }

    /// <summary>
    /// Gets or sets covid total cases
    /// </summary>
    public class Total
    {
        /// <summary>
        /// Gets or sets covid total cases value
        /// </summary>
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}

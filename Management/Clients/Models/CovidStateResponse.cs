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
        /// Gets or sets confirmed covid cases
        /// </summary>
        [JsonProperty("positive")]
        public int Positive { get; set; }
    }
}

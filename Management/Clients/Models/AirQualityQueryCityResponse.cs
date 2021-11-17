using Newtonsoft.Json;
using System.Collections.Generic;

namespace Management.Clients.Models
{
    public class AirQualityQueryCityResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<CityData> Data { get; set; }
    }

    public class CityData
    {
        [JsonProperty("city")]
        public string CityName { get; set; }
    }
}

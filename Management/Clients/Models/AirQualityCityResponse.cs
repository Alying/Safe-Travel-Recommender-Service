using Newtonsoft.Json;

namespace Management.Clients.Models
{
    public class AirQualityCityResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonIgnore]
        public Location Location { get; set; }

        [JsonIgnore]
        public Forecasts Forecasts { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }
    }

    public class Location
    {
    }

    public class Forecasts
    {
    }

    public class Current
    {
        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        [JsonProperty("pollution")]
        public Pollution Pollution { get; set; }
    }

    public class Weather
    {
        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("tp")]
        public double Tp { get; set; }

        [JsonProperty("pr")]
        public double Pr { get; set; }

        [JsonProperty("hu")]
        public double Hu { get; set; }

        [JsonProperty("ws")]
        public double Ws { get; set; }

        [JsonProperty("wd")]
        public double Wd { get; set; }

        [JsonProperty("ic")]
        public string Ic { get; set; }
    }

    public class Pollution
    {
        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("aqius")]
        public double Aqius { get; set; }

        [JsonProperty("mainus")]
        public string Mainus { get; set; }

        [JsonProperty("aqicn")]
        public double Aqicn { get; set; }

        [JsonProperty("maincn")]
        public string Maincn { get; set; }
    }
}

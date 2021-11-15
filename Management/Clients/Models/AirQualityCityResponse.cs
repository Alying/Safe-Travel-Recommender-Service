using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.Clients.Models
{
    public class AirQualityCityResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public Data Data{ get; set; }
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
        public Location Location{ get; set; }

        [JsonIgnore]
        public Forecasts Forecasts { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }
    }

    public class Location { }

    public class Forecasts { }

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
        public int Tp { get; set; }

        [JsonProperty("pr")]
        public int Pr { get; set; }

        [JsonProperty("hu")]
        public int Hu { get; set; }

        [JsonProperty("ws")]
        public int Ws { get; set; }

        [JsonProperty("wd")]
        public int Wd { get; set; }

        [JsonProperty("ic")]
        public string Ic { get; set; }
    }

    public class Pollution
    {
        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("aqius")]
        public int Aqius { get; set; }

        [JsonProperty("mainus")]
        public string Mainus { get; set; }

        [JsonProperty("aqicn")]
        public int Aqicn { get; set; }

        [JsonProperty("maincn")]
        public string Maincn { get; set; }
    }
}

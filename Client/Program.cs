using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client
{
    public class ClientResponse
    {
        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("recommendationState")]
        public string RecommendationState { get; set; }

        [JsonPropertyName("overallScore")]
        public float OverAllScore { get; set; }

        [JsonPropertyName("airQualityScore")]
        public float AirQualityScore { get; set; }

        [JsonPropertyName("covidIndexScore")]
        public float CovideIndexScore { get; set; }

        [JsonPropertyName("weatherScore")]
        public float WeatherScore { get; set; }
    }

    class Program
    {
        private static async Task<List<ClientResponse>> Client()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            string url = "https://localhost:5001/api/recommendations";
            var streamTask = await client.GetStreamAsync(url);
            Console.WriteLine(streamTask.ToString());
            var responses = await System.Text.Json.JsonSerializer.DeserializeAsync<List<ClientResponse>>(streamTask);
            return responses;
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting client...");

            // start client with an enter after service is fully running
            Console.ReadLine();

            var task = await Client();
            foreach (var itm in task)
                Console.WriteLine($"{itm.State}, {itm.RecommendationState}, {itm.OverAllScore}");
        }
    }
}

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
        public float OverallScore { get; set; }

        [JsonPropertyName("airQualityScore")]
        public float AirQualityScore { get; set; }

        [JsonPropertyName("covidIndexScore")]
        public float CovidIndexScore { get; set; }

        [JsonPropertyName("weatherScore")]
        public float WeatherScore { get; set; }
    }

    class Program
    {
        private static async Task<List<ClientResponse>> RecommendationClient(string endpoint)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var streamTask = await client.GetStreamAsync(endpoint);
            var response = await System.Text.Json.JsonSerializer.DeserializeAsync<List<ClientResponse>>(streamTask);
            return response;
        }
        private static async Task<ClientResponse> LocationInquiryClient(string endpoint)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            var streamTask = await client.GetStreamAsync(endpoint);
            var response = await System.Text.Json.JsonSerializer.DeserializeAsync<ClientResponse>(streamTask);
            return response;
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting client...");

            // can hit either recommendation or location inquiry endpoint from Safe Travel Service
            string recommendationEndpoint = "https://localhost:5001/api/recommendations";
            string locationInquiryEndpoint = "https://localhost:5001/api/recommendations/country/US/state/California";

            // start location inquiry client with an enter after service is fully running
            Console.ReadLine();
            var locationInquiryTask = await LocationInquiryClient(locationInquiryEndpoint);
            Console.WriteLine($"{locationInquiryTask.State}, {locationInquiryTask.RecommendationState}, {locationInquiryTask.OverallScore}");

            // start recommendation client with another enter
            Console.ReadLine();
            var recommendationTask = await RecommendationClient(recommendationEndpoint);
            foreach (var item in recommendationTask)
                Console.WriteLine($"{item.State}, {item.RecommendationState}, {item.OverallScore}");

            // TODO: @alinaying, make console app do something with the retrieved responses
        }
    }
}

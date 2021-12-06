using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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

    public class Program
    {
        public static string url = "http://localhost:8000/";
        public static HttpListener listener;
        public static int requestCount = 0;
        public static int pageViews = 0;
        public static string pageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>HttpListener Example</title>" +
            "  </head>" +
            "  <body>" +
            "    <p>Page Views: {0}</p>" +
            "    <form method=\"post\" action=\"shutdown\">" +
            "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
            "    </form>" +
            "  </body>" +
            "</html>";

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

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;
            while (runServer)
            {
                HttpListenerContext ctx = await listener.GetContextAsync();
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;
                Console.WriteLine($"{++requestCount}, {req.Url.ToString()}, {req.HttpMethod}, {req.UserHostName}, {req.UserAgent}");

                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath) == "shutdown")
                {
                    Console.WriteLine("SHutdown requ4ested");
                    runServer = false;
                }

                if (req.Url.AbsolutePath != "/favicon.ico")
                {
                    pageViews += 1;
                }

                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

            }
        }

        private static async Task Main(string[] args)
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
            // create a service
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine($"Listening to {url}");

            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();
            listener.Close();

            //Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}

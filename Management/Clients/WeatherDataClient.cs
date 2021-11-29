using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Clients.Models;
using Management.DomainModels;
using Management.Enum;
using Management.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Management.Clients
{
    /// <summary>
    /// Representation of the client for the weather API.
    /// </summary>
    public class WeatherDataClient : IWeatherDataClient
    {
        private readonly IConfiguration _configuration;

        private IRestClient _restClient;

        private string ApiKey => _configuration.GetConnectionString("weatherApiKey");

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherDataClient"/> class.
        /// </summary>
        /// <param name="configuration">configuration for weather data client.</param>
        public WeatherDataClient(IConfiguration configuration)
        {
            _restClient = new RestClient("http://api.openweathermap.org");
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Retrieve city data from weather api
        /// </summary>
        /// <param name="city">city of interest eg. NYC.</param>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        public async Task<WeatherCityResponse> GetCityWeatherDataAsync(
            City city,
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var request = new RestRequest("data/2.5/weather", Method.GET);
            request
                .AddQueryParameter("q", city.Value + "," + state.Value + "," + countryCode)
                .AddQueryParameter("appid", ApiKey);

            var response = await _restClient.ExecuteAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<WeatherCityResponse>(response.Content);
            }

            // According to air visual api's free package, we can only make 60 calls per minute. However, I tested
            // several times with hundreds of calls but never hit too many request(429 error code according to their website).
            // I will comment this part out for now, and we can reuse it if we need to.

            //else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            //{
            //    return new WeatherCityResponse
            //    {
            //        Status = "success",
            //        WeatherData = new WeatherData
            //        {
            //            // usually temperature will fall in between 0 - 40 Celsius or 273 - 313 Kelvin
            //            Temp = new Random().Next(273, 313),
            //        },
            //    };
            //}

            throw new Exception($"Failed to get data from open weather. Error detail: {response.Content}");
        }

        /// <summary>
        /// Use temperature to calculate score of a state
        /// </summary>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        public async Task<(State, double)> CalculateScoreForStateAsync(
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var cityBag = new ConcurrentBag<(City city, int score)>();

            var supportedCities = AbbreviationToState.GetSupportedCities(state.Value);
            var cityTasks = supportedCities.Select(async city =>
            {
                var result = await GetSingleCityAsync(city, state, countryCode, cancellationToken);
                cityBag.Add(result);
            });
            await Task.WhenAll(cityTasks);


            return (state, cityBag.Select(res => res.score).Sum() / cityBag.Count());
        }

        /// <summary>
        /// Get single city's weather score
        /// </summary>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        private async Task<(City city, int score)> GetSingleCityAsync(
            City city,
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var result = await GetCityWeatherDataAsync(city, state, countryCode, cancellationToken);

            var tempInK = result?.WeatherData?.Temp;
            
            if (tempInK == null)
            {
                throw new Exception("Received null response from vendor.");
            }

            // Convert temperature in Kelvin to temperature in Fahrenheit
            var tempInF = (((tempInK - 273.15) * 9) / 5) + 32;

            // Human's ideal temperature is around 70 Fahrenheit
            // Deduct 2 points from score 100 for each temperature point that's off from the ideal temperature
            return (city, Math.Max(0, (int)(100 - (2 * Math.Abs((decimal)tempInF - 70)))));
        }
    }
}

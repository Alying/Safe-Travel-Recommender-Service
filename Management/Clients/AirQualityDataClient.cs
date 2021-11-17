using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
    /// Representation of the client for the Air Quality API.
    /// </summary>
    public class AirQualityDataClient : IAirQualityDataClient
    {
        private readonly IConfiguration _configuration;

        private IRestClient _restClient;

        private string ApiKey => _configuration.GetConnectionString("airApiKey");

        public AirQualityDataClient(IConfiguration configuration)
        {
            _restClient = new RestClient("http://api.airvisual.com");
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<AirQualityCityResponse> GetAirQualityAsync(
            City city,
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var request = new RestRequest("v2/city", Method.GET);
            request
                .AddQueryParameter("city", city.Value)
                .AddQueryParameter("state", state.Value)
                .AddQueryParameter("country", countryCode == CountryCode.US ? "USA" : "CANADA")
                .AddQueryParameter("key", ApiKey);

            var response = await _restClient.ExecuteAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<AirQualityCityResponse>(response.Content);
            }

            throw new Exception("Retrieve Data Failed");
        }

        // Use Air Quality Index
        // https://www.airnow.gov/aqi/aqi-basics/
        public async Task<Dictionary<City, int>> CalculateScoresAsync(
            IEnumerable<City> cities,
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var bag = new ConcurrentBag<(City city, int score)>();

            var tasks = cities.Select(async city =>
            {
                var response = await GetSingleCityAsync(city, state, countryCode, cancellationToken);
                bag.Add(response);
            });

            await Task.WhenAll(tasks);
            var count = bag.Count;

            return bag
                .ToLookup(b => b.city)
                .ToDictionary(l => l.Key, l => l.First().score);
        }

        public async Task<IEnumerable<City>> GetDefaultCitiesAsync(
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var request = new RestRequest("v2/cities", Method.GET);
            request
                .AddQueryParameter("state", state.Value)
                .AddQueryParameter("country", countryCode == CountryCode.US ? "USA" : "CANADA")
                .AddQueryParameter("key", ApiKey);

            var response = await _restClient.ExecuteAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert
                    .DeserializeObject<AirQualityQueryCityResponse>(response.Content)
                    .Data
                    .Select(cityData => City.Wrap(cityData.CityName))
                    .Take(10);
            }

            throw new Exception("Retrieve Data Failed");
        }

        private async Task<(City city, int score)> GetSingleCityAsync(
            City city,
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var result = await GetAirQualityAsync(city, state, countryCode, cancellationToken);

            var aqius = result?.Data?.Current?.Pollution?.Aqius;

            if (aqius == null)
            {
                throw new Exception("Received null response from vendor.");
            }

            if (0 <= aqius && aqius <= 50)
            {
                return (city, 100);
            }

            if (50 <= aqius && aqius <= 100)
            {
                return (city, 80);
            }

            if (100 <= aqius && aqius <= 150)
            {
                return (city, 60);
            }

            if (150 <= aqius && aqius <= 200)
            {
                return (city, 40);
            }

            if (200 <= aqius && aqius <= 300)
            {
                return (city, 20);
            }

            if (300 <= aqius)
            {
                return (city, 0);
            }

            return (city, 0);
        }
    }
}

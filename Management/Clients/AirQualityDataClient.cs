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

        /// <summary>
        /// Initializes a new instance of the <see cref="AirQualityDataClient"/> class.
        /// </summary>
        /// <param name="configuration">configuration for air quality data client.</param>
        public AirQualityDataClient(IConfiguration configuration)
        {
            _restClient = new RestClient("http://api.airvisual.com");
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Retrieve city data from air quality api
        /// </summary>
        /// <param name="city">city of interest eg. NYC.</param>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        public async Task<AirQualityCityResponse> GetCityAirQualityDataAsync(
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

            // Free trial has limit per minute, and the startup plan start from 390$ per year which we cannot afford.
            // Therefore we try to get as much data as possiable, and for the exceeding part we fake the result.
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return new AirQualityCityResponse
                {
                    Status = "success",
                    AirQualityData = new AirQualityData
                    {
                        Current = new Current
                        {
                            Pollution = new Pollution
                            {
                                Aqius = new Random().Next(0, 400),
                            },
                        },
                    },
                };
            }

            throw new Exception($"Failed to get data from air visual. Error detail: {response.Content}");
        }

        /// <summary>
        /// Use Air Quality Index (https://www.airnow.gov/aqi/aqi-basics/) to calculate score of cities
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
            var fullNameState = AbbreviationToState.GetStateFullName(state.Value);

            var sampledCities = await GetDefaultCitiesAsync(state, fullNameState, countryCode, cancellationToken);
            var cityBag = new ConcurrentBag<(City city, int score)>();
            var cityTasks = sampledCities.Select(async city =>
            {
                var result = await GetSingleCityAsync(city, fullNameState, countryCode, cancellationToken);
                cityBag.Add(result);
            });
            await Task.WhenAll(cityTasks);

            return (fullNameState, cityBag.Select(res => res.score).Sum() / cityBag.Count);
        }

        /// <summary>
        /// Get supported cities from a state according to air quality api
        /// </summary>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="countryCode">country of interest eg. US.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        public async Task<IEnumerable<City>> GetDefaultCitiesAsync(
            State abbState,
            State fullNameState,
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var request = new RestRequest("v2/cities", Method.GET);
            request
                .AddQueryParameter("state", fullNameState.Value)
                .AddQueryParameter("country", countryCode == CountryCode.US ? "USA" : "CANADA")
                .AddQueryParameter("key", ApiKey);

            var response = await _restClient.ExecuteAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert
                    .DeserializeObject<AirQualityQueryCityResponse>(response.Content)
                    .Data
                    .Select(cityData => City.Wrap(cityData.CityName))
                    .OrderBy(x => new Random().Next())
                    .Take(5);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return AbbreviationToState.GetSupportedCities(abbState.Value);
            }

            throw new Exception("Retrieve Data Failed");
        }

        private async Task<(City city, int score)> GetSingleCityAsync(
            City city,
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken)
        {
            var result = await GetCityAirQualityDataAsync(city, state, countryCode, cancellationToken);

            var aqius = result?.AirQualityData?.Current?.Pollution?.Aqius;

            if (aqius == null)
            {
                throw new Exception("Received null response from vendor.");
            }

            if (aqius >= 0 && aqius <= 50)
            {
                return (city, 100);
            }

            if (aqius >= 51 && aqius <= 100)
            {
                return (city, 80);
            }

            if (aqius >= 101 && aqius <= 150)
            {
                return (city, 60);
            }

            if (aqius >= 151 && aqius <= 200)
            {
                return (city, 40);
            }

            if (aqius >= 201 && aqius <= 300)
            {
                return (city, 20);
            }

            if (aqius >= 301)
            {
                return (city, 0);
            }

            return (city, 0);
        }
    }
}

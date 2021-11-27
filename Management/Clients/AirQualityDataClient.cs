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
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return new AirQualityCityResponse
                {
                    Status = "success",
                    Data = new Data
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
        /// Use Air Quality Index (https://www.airnow.gov/aqi/aqi-basics/) to calculate score of a state
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
            var abbrStateDict = new AbbrToStateDict();
            var stateCityDict = new StateToCityDict();
            var supportedCities = stateCityDict.stateToCityDict[state.Value];

            var cityBag = new ConcurrentBag<(City city, int score)>();
            var cityTasks = supportedCities.Select(async city =>
            {
                var result = await GetSingleCityAsync(city, abbrStateDict.abbrToStateDict[state.Value], countryCode, cancellationToken);
                cityBag.Add(result);
            });
            await Task.WhenAll(cityTasks);

            return (state, cityBag.Select(res => res.score).Sum() / cityBag.Count());
        }

        /// <summary>
        /// Get single city's air quality score
        /// </summary>
        /// <param name="city">city of interest eg. NYC.</param>
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
            var result = await GetCityAirQualityDataAsync(city, state, countryCode, cancellationToken);
            
            var aqius = result?.Data?.Current?.Pollution?.Aqius;

            if (aqius == null)
            {
                throw new Exception("Received null response from vendor.");
            }

            if (aqius >= 0 && aqius <= 50)
            {
                return (city, 100);
            }

            if (aqius >= 50 && aqius <= 100)
            {
                return (city, 80);
            }

            if (aqius >= 100 && aqius <= 150)
            {
                return (city, 60);
            }

            if (aqius >= 150 && aqius <= 200)
            {
                return (city, 40);
            }

            if (aqius >= 200 && aqius <= 300)
            {
                return (city, 20);
            }

            if (aqius >= 300)
            {
                return (city, 0);
            }

            return (city, 0);
        }
    }
}

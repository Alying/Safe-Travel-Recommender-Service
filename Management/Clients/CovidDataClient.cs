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
    /// Representation of the client for the covid API.
    /// </summary>
    public class CovidDataClient : ICovidDataClient
    {
        private readonly IConfiguration _configuration;

        private IRestClient _restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CovidDataClient"/> class.
        /// </summary>
        /// <param name="configuration">configuration for weather data client.</param>
        public CovidDataClient(IConfiguration configuration)
        {
            _restClient = new RestClient("https://api.covidtracking.com");
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Retrieve city data from covid api
        /// </summary>
        /// <param name="state">state of interest eg. NY.</param>
        /// <param name="cancellationToken">used to signal that the asynchronous task should cancel itself.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        public async Task<CovidStateResponse> GetStateCovidDataAsync(
            State state,
            CancellationToken cancellationToken)
        {
            // Covid Tracking Project stop collecting new COVID data since 2021-03-07
            var request = new RestRequest($"v2/states/{state.Value.ToLower()}/2021-03-07.json", Method.GET);

            var response = await _restClient.ExecuteAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<CovidStateResponse>(response.Content);
            }

            throw new Exception($"Failed to get data from covid tracking project. Error detail: {response.Content}");
        }

        /// <summary>
        /// Use covid-19 confirmed cases to calculate score of state
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
            var result = await GetStateCovidDataAsync(state, cancellationToken);

            var confirmedCases = result?.CovidData?.Cases?.Total?.Value;

            if (confirmedCases == null)
            {
                throw new Exception("Received null response from vendor.");
            }

            if (confirmedCases >= 0 && confirmedCases <= 200000)
            {
                return (state, 100);
            }

            if (confirmedCases >= 200001 && confirmedCases <= 500000)
            {
                return (state, 80);
            }

            if (confirmedCases >= 500001 && confirmedCases <= 700000)
            {
                return (state, 60);
            }

            if (confirmedCases >= 700001 && confirmedCases <= 1000000)
            {
                return (state, 40);
            }

            if (confirmedCases >= 1000001 && confirmedCases <= 2000000)
            {
                return (state, 20);
            }

            return (state, 0);
        }
    }
}

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
        public async Task<CovidResponse> GetStateCovidDataAsync(
            State state,
            CancellationToken cancellationToken)
        {
            var request = new RestRequest("v2/states/" + state.Value + "/daily.json", Method.GET);

            var response = await _restClient.ExecuteAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<CovidResponse>(response.Content);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return new CovidResponse
                {
                    CovidDataList = new List<CovidData>
                    {
                        new CovidData
                        {
                            Cases = new Cases
                            {
                                Confirmed = new Confirmed
                                {
                                    Value = new Random().Next(0, 99999999),
                                },
                            },
                        },
                    },
                };
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
            var stateBag = new ConcurrentBag<(State city, int score)>();

            var result = await GetSingleStateAsync(State.Wrap("me"), cancellationToken);
            stateBag.Add(result);

            return (state, stateBag.Select(res => res.score).Sum() / stateBag.Count());
        }

        private async Task<(State state, int score)> GetSingleStateAsync(
            State state,
            CancellationToken cancellationToken)
        {
            var result = await GetStateCovidDataAsync(state, cancellationToken);

            var confirmedCases = result?.CovidDataList[0]?.Cases?.Confirmed?.Value;

            if (confirmedCases == null)
            {
                throw new Exception("Received null response from vendor.");
            }
            Console.WriteLine(confirmedCases);
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

            if (confirmedCases >= 2000001)
            {
                return (state, 0);
            }

            return (state, 0);
        }
    }
}

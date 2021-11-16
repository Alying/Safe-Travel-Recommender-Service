using System;
using System.Threading;
using System.Threading.Tasks;
using Management.Clients.Models;
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
        private IRestClient _restClient;

        private readonly IConfiguration _configuration;

        private string ApiKey => _configuration.GetConnectionString("airApiKey");

        public AirQualityDataClient(IConfiguration configuration)
        {
            _restClient = new RestClient("http://api.airvisual.com");
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<AirQualityCityResponse> GetAirQualityAsync(string city, string state, string countryCode, CancellationToken cancellationToken)
        {
            var request = new RestRequest("v2/city", Method.GET);
            request
                .AddQueryParameter("city", city)
                .AddQueryParameter("state", state)
                .AddQueryParameter("country", countryCode)
                .AddQueryParameter("key", ApiKey);

            var response = await _restClient.ExecuteAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<AirQualityCityResponse>(response.Content);
            }

            throw new Exception("Retrieve Data Failed");
        }

        public int CalculateScoreAsync()
            => throw new NotImplementedException();
    }
}
